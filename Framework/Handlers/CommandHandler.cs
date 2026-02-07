using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Handlers
{
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public abstract class AuthenticatedCommandHandler<TRequest> : CommandHandler<TRequest> {}
    
    public abstract class CommandHandler<TRequest> : BaseHandler<TRequest>
    {
        [HttpPost]
        [Tags("[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CommandExecute([FromBody] TRequest parameters)
        {
            var inboxMessageId = await AddReceivedLog(parameters);
            
            ValidateInputParameters();

            try
            {
                await Execute(parameters);
            }
            catch (BadHttpRequestException e)
            {
                await FinishReceivedLog(inboxMessageId, e, 400);
                return Problem(statusCode: 400, detail: e.Message);
            }
            catch (Exception e)
            {
                await FinishReceivedLog(inboxMessageId, e, 500);
                return Problem(statusCode: 500, detail: e.Message);
            }
            
            await FinishReceivedLog(inboxMessageId, null, 200);
            
            return Ok();
        }

        protected abstract Task Execute(TRequest request);
    }
}