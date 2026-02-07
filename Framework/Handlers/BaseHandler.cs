using System.Security.Claims;
using AutoMapper;
using Framework.Database;
using Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Handlers;

public abstract class BaseHandler<TRequest> : ControllerBase
{
    private IMapper? _mapper;
    private FrameworkDbContext? _db;

    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
    private FrameworkDbContext Db => _db ??= HttpContext.RequestServices.GetRequiredService<FrameworkDbContext>();

    protected async Task<Guid> AddReceivedLog(TRequest parameters)
    {
        var data = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
        var userIdKnown = Guid.TryParse(HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId);
        var receivedLog = new InboxMessage
        {
            Type = Request.Path.HasValue ? Request.Path.Value : "Unknown",
            Content = data,
            Source = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            SourceType = Request.Headers.FirstOrDefault(h => h.Key.Equals("X-ApplicationId", StringComparison.CurrentCultureIgnoreCase)).Value,
            UserName = HttpContext.User?.Identity?.Name,
            UserId = userIdKnown ? userId : null
        };
            
        var response = Db.InboxMessage.Add(receivedLog);
        await Db.SaveChangesAsync();

        return response.Entity.InboxMessageId;
    }

    protected void ValidateInputParameters()
    {
        string requestName = typeof(TRequest).ToString().Split('.').Last();
        string className = requestName + "Validator";
        Type? type = Type.GetType(className);
        if (type is not null)
        {
            object? obj = Activator.CreateInstance(type);
        }
    }

    protected async Task FinishReceivedLog(Guid inboxMessageId, Exception? e = null, int? status = null)
    {
        var inboxMessage = Db.InboxMessage.FirstOrDefault(im => im.InboxMessageId == inboxMessageId);

        if (inboxMessage is not null)
        {
            inboxMessage.Handled = DateTime.UtcNow;
            inboxMessage.Success = e is null;
            inboxMessage.Status = status.ToString();
            inboxMessage.Error = e?.Message;
                
            Db.InboxMessage.Update(inboxMessage);
        }
            
        await Db.SaveChangesAsync();
    }
}