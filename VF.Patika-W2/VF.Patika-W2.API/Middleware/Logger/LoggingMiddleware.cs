namespace VF.Patika_W2.API.Middleware.Logger
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine($"Actiona girdi: {context.Request.Path}");
            await _next(context);
        }
    }
}
