using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        // permet de renvoyer la requête a l'etape suivante
        private readonly RequestDelegate _next;

        // permet de logger les erreurs
        private readonly ILogger<ExceptionMiddleware> _logger;

        // permet de savoir si l'application est en mode développement ou production
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(message : "Une exception à été captureée",args : ex);

                context.Response.ContentType = "application/json";

                switch(ex)
                {
                    case UnauthorizedAccessException: 
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var errorResponse = new
                {
                    succes = false,
                    errorMessage = _env.IsDevelopment() ?  ex.Message : "Une erreur s'est produite, veuillez réessayer plus tard."
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }

    }
}
