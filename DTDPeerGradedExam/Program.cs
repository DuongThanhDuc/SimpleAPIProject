using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using utilities;
using services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(options =>
    {
        options.LoggingFields = HttpLoggingFields.All;
    });

builder.Services.AddSingleton<DataSeeder>();
builder.Services.AddSingleton<UserServices>();
builder.Services.AddSingleton<ProductServices>();
builder.Services.AddSingleton<OrderServices>();
builder.Services.AddSingleton<OrderDetailServices>();


var app = builder.Build();

await app.Services.GetRequiredService<DataSeeder>().DataSeeders();

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