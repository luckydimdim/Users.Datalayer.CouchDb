using Cmas.BusinessLayers.Users.CommandsContexts;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace Cmas.DataLayers.CouchDb.Users.Commands
{
    public class DeleteUserCommand : ICommand<DeleteUserCommandContext>
    {
        private readonly CouchWrapper _couchWrapper;

        public DeleteUserCommand(IServiceProvider serviceProvider)
        {
            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<DeleteUserCommandContext> Execute(DeleteUserCommandContext commandContext)
        {
            var header = await _couchWrapper.GetHeaderAsync(commandContext.Id);

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Documents.DeleteAsync(commandContext.Id, header.Rev);
            });

            return commandContext;
        }
    }
}