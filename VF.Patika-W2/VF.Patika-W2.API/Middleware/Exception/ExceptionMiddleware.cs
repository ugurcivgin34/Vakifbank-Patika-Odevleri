namespace VF.Patika_W2.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}"); // Bu gerçekte bir loglama servisine yazılmalı.
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Bir hata oluştu, lütfen daha sonra tekrar deneyin.");
            }
        }
    }
}
