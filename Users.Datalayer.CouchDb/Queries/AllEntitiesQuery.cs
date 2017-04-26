using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Users.Entities;
using Cmas.DataLayers.CouchDb.Users.Dtos;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using Microsoft.Extensions.Logging;
using MyCouch.Requests;

namespace Cmas.DataLayers.CouchDb.Users.Queries
{
    public class AllEntitiesQuery : IQuery<AllEntities, Task<IEnumerable<User>>>
    {
        private IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public AllEntitiesQuery(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<AllEntitiesQuery>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<IEnumerable<User>> Ask(AllEntities criterion)
        {
            var result = new List<User>();

            var query = new QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.AllDocsViewName);

            var viewResult = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Views.QueryAsync<UserDto>(query);
            });

            foreach (var row in viewResult.Rows.OrderByDescending(s => s.Value.Login))
            {
                result.Add(_autoMapper.Map<User>(row.Value));
            }

            return result;
        }
    }
}