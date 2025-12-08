using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using utilities;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(options =>
    {
        options.LoggingFields = HttpLoggingFields.All;
    });

builder.Services.AddSingleton<UserServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Swagger at root
    });
}

app.UseExceptionHandler("/error");

app.UseHttpLogging();

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode >= 400)
    {
        Console.WriteLine(
            $"[SECURITY] Path: {context.Request.Path}, Status: {context.Response.StatusCode}"
        );
    }
});

app.MapControllers();

app.Run();