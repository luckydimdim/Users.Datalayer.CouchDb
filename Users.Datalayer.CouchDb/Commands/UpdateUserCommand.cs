using System.Threading.Tasks;
using AutoMapper;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.DataLayers.Infrastructure;
using Microsoft.Extensions.Logging;
using Cmas.DataLayers.CouchDb.Users;
using Cmas.DataLayers.CouchDb.Users.Dtos;
using Cmas.BusinessLayers.Users.CommandsContexts;

namespace Cmas.DataLayers.CouchDb.Requests.Commands
{
    public class UpdateUserCommand : ICommand<UpdateUserCommandContext>
    {
        private IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public UpdateUserCommand(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<UpdateUserCommand>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<UpdateUserCommandContext> Execute(UpdateUserCommandContext commandContext)
        {
            // FIXME: нельзя так делать, надо от frontend получать Rev
            var header = await _couchWrapper.GetHeaderAsync(commandContext.User.Id);

            var entity = _autoMapper.Map<UserDto>(commandContext.User);

            entity._rev = header.Rev;

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.PutAsync(entity._id, entity);
            });

            return commandContext; // TODO: возвращать _revid
        }
    }
}