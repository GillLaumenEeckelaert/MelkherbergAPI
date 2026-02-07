using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Handlers
{
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public abstract class AuthenticatedUploadHandler<TRequest> : UploadHandler<TRequest>
    {
    }

    public abstract class UploadHandler<TRequest> : BaseHandler<TRequest>
    {
        [HttpPost]
        [Tags("[controller]")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UploadExecute([FromForm] TRequest parameters)
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

        protected async Task<string> UploadFile(IFormFile file)
        {
            const string folderName = "Media";
            const string basePath = "/Users/gill/Documents/Projects/TestServer";
            
            var folderPath = Path.Combine(basePath, folderName);
            
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
            var filePath = Path.Combine(folderPath, file.FileName);

            await using var fs = System.IO.File.Create(filePath);
            await file.CopyToAsync(fs);
            
            return filePath;
        }
        
        protected abstract Task Execute(TRequest request);
    }
}