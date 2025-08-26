using System.Linq.Expressions;

namespace eCommerce.API.MiddleWare
{
    public class ExceptionHandlingMiddleware  
    {

        private readonly RequestDelegate _context;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;   

        public ExceptionHandlingMiddleware(RequestDelegate context, ILogger<ExceptionHandlingMiddleware> logger)
        {
             _context = context;    
             _logger = logger;   
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _context(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.GetType().ToString()}:{ex.Message}");
                 await httpContext.Response.WriteAsJsonAsync(ex.Message);
            }

        }

    }

}
