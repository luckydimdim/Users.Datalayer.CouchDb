using AutoMapper;
using Cmas.BusinessLayers.Users.CommandsContexts;
using Cmas.DataLayers.CouchDb.Users.Dtos;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace Cmas.DataLayers.CouchDb.Users.Commands
{
    public class CreateUserCommand : ICommand<CreateUserCommandContext>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public CreateUserCommand(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper) serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<CreateUserCommandContext> Execute(CreateUserCommandContext commandContext)
        {
            var doc = _autoMapper.Map<UserDto>(commandContext.User);

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.PostAsync(doc);
            });

            commandContext.Id = result.Id;

            return commandContext;
        }
    }
}