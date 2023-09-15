using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Net;
using VF.Patika_W2.API.Middleware;
using VF.Patika_W2.API.Middleware.Logger;
using VF.Patika_W2.API.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        // Yanýtýn türünü JSON olarak ayarlar.
        context.Response.ContentType = "application/json";

        // Hata detaylarýný alýr.
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature != null)
        {
            if (contextFeature.Error is ValidationException)
            {
                // Geçerlilik istisnasý oluþtuysa, HTTP 400 Bad Request yanýtý gönderir.
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = contextFeature.Error.Message
                }.ToString());
            }
            else
            {
                // Diðer türde hata oluþtuysa, HTTP 500 Internal Server Error yanýtý gönderir.
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(new
                {
                    status = HttpStatusCode.InternalServerError,
                    message = "Internal Server Error."
                }.ToString());
            }
        }
    });
});

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
