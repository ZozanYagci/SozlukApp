using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Advanced;
using Microsoft.Identity.Client.Extensibility;
using SozlukApp.Api.Application.Extensions;
using SozlukApp.Api.WebApi.Infrastructure.Extensions;
using SozlukApp.Infrastructure.Persistence.Extensions;
using System.Threading.Tasks.Dataflow;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddFluentValidation();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);

//Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();


}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptionHandling(app.Environment.IsDevelopment());


app.UseAuthentication();
app.UseAuthorization();


app.UseCors("MyPolicy");
app.MapControllers();

app.Run();
