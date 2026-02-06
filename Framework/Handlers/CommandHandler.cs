using AutoMapper;
using Framework.Database;
using Framework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Handlers
{
    public abstract class QueryHandler<TRequest, TResponse> : ControllerBase
    {
        private IMapper? _mapper;
        private FrameworkDbContext? _db;

        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
        private FrameworkDbContext Db => _db ??= HttpContext.RequestServices.GetRequiredService<FrameworkDbContext>();

        [HttpGet]
        [Tags("[controller]")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TResponse>> QueryExecute([FromQuery] TRequest parameters)
        {
            var inboxMessageId = AddReceivedLog();
            
            ValidateInputParameters();
            TResponse response = await Execute(parameters);
            
            FinishReceivedLog(inboxMessageId, response);
            
            return Ok(response);
        }

        protected abstract Task<TResponse> Execute(TRequest request);

        private Guid AddReceivedLog()
        {
            var request = HttpContext.Request;
            
            var receivedLog = new InboxMessage
            {
                Type = request.Path.HasValue ? request.Path.Value : "Unknown",
                Content = request.QueryString.HasValue ? request.QueryString.Value : string.Empty,
                Source = request.HttpContext.Connection.RemoteIpAddress?.ToString()
            };
            
            var response = Db.InboxMessage.Add(receivedLog);
            Db.SaveChanges();

            return response.Entity.InboxMessageId;
        }
        
        private void ValidateInputParameters()
        {
            string requestName = typeof(TRequest).ToString().Split('.').Last();
            string className = requestName + "Validator";
            Type? type = Type.GetType(className);
            if (type is not null)
            {
                object? obj = Activator.CreateInstance(type);
            }
        }

        private void FinishReceivedLog(Guid inboxMessageId, TResponse? response)
        {
            var inboxMessage = Db.InboxMessage.FirstOrDefault(im => im.InboxMessageId == inboxMessageId);

            if (inboxMessage is not null)
            {
                inboxMessage.Handled = DateTime.UtcNow;
                inboxMessage.Success = response is not null;
                
                Db.InboxMessage.Update(inboxMessage);
            }
            
            Db.SaveChanges();
        }
    }
}