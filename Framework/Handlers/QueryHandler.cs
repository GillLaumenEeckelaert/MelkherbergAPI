using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Handlers
{
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public abstract class AuthenticatedQueryHandler<TRequest, TResponse> : QueryHandler<TRequest, TResponse> {}
    
    public abstract class QueryHandler<TRequest, TResponse> : BaseHandler<TRequest>
    {
        [HttpGet]
        [Tags("[controller]")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TResponse>> QueryExecute([FromQuery] TRequest parameters)
        {
            var inboxMessageId = await AddReceivedLog(parameters);
            
            ValidateInputParameters();

            TResponse response;

            try
            {
                response = await Execute(parameters);
            }
            catch (Exception e)
            {
                await FinishReceivedLog(inboxMessageId, e, 500);
                return Problem(statusCode: 500, detail: e.Message);
            }
            
            await FinishReceivedLog(inboxMessageId, null, 200);
            
            return Ok(response);
        }

        protected abstract Task<TResponse> Execute(TRequest request);
    }
}