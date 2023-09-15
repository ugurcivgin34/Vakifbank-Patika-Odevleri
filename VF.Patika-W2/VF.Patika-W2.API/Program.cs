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
        // Yan�t�n t�r�n� JSON olarak ayarlar.
        context.Response.ContentType = "application/json";

        // Hata detaylar�n� al�r.
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature != null)
        {
            if (contextFeature.Error is ValidationException)
            {
                // Ge�erlilik istisnas� olu�tuysa, HTTP 400 Bad Request yan�t� g�nderir.
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = contextFeature.Error.Message
                }.ToString());
            }
            else
            {
                // Di�er t�rde hata olu�tuysa, HTTP 500 Internal Server Error yan�t� g�nderir.
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
