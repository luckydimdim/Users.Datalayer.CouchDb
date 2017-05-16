using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Users.Entities;
using Cmas.DataLayers.CouchDb.Users.Dtos;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Cmas.DataLayers.CouchDb.Users.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<User>>
    {
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public FindByIdQuery(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<User> Ask(FindById criterion)
        {
            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.GetAsync<UserDto>(criterion.Id);
            });

            return _autoMapper.Map<User>(result.Content);
        }
    }
}