using api.Database;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Framework.Handlers
{
    public abstract class QueryHandler<TRequest, TResponse> : ControllerBase
    {
        private ApplicationDBContext? _db;
        private IMapper? _mapper;

        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
        protected ApplicationDBContext Db => _db ??= HttpContext.RequestServices.GetRequiredService<ApplicationDBContext>();

        [HttpGet]
        [Tags("[controller]")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TResponse>> QueryExecute([FromQuery] TRequest parameters)
        {
            ValidateInputParameters();
            TResponse response = await Execute(parameters);
            return Ok(response);
        }

        protected abstract Task<TResponse> Execute(TRequest request);

        private void ValidateInputParameters()
        {
            Console.WriteLine(typeof(TRequest).ToString());
            string requestName = typeof(TRequest).ToString().Split('.').Last();
            string className = requestName + "Validator";
            Console.WriteLine(className);
            Type? type = Type.GetType("api.Handlers.Logs.GetPublicLogValidator");
            Console.WriteLine(type);
            if (type is not null)
            {
                object? obj = Activator.CreateInstance(type);
                Console.WriteLine(obj);
            }
        }
    }
}