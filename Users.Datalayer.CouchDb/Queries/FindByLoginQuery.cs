using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Users.Criteria;
using Cmas.BusinessLayers.Users.Entities;
using Cmas.DataLayers.CouchDb.Users.Dtos;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Queries;
using Microsoft.Extensions.Logging;
using CouchRequest = MyCouch.Requests;

namespace Cmas.DataLayers.CouchDb.Users.Queries
{
    public class FindByLoginQuery : IQuery<FindByLogin, Task<User>>
    {
        private IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public FindByLoginQuery(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<User> Ask(FindByLogin criterion)
        {
            var query =
                new CouchRequest.QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.ByLoginDocsViewName)
                    .Configure(q => q.Key(criterion.Login));

            var viewResult = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Views.QueryAsync<UserDto>(query);
            });

            if (viewResult.Rows.Length == 0)
                return null;

            if (viewResult.Rows.Length > 1)
                throw new Exception(String.Format("several records found by login {0}", criterion.Login));

            return _autoMapper.Map<User>(viewResult.Rows.Single().Value);
        }
    }
}