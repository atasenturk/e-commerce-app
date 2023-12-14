using E_CommerceApp.Api.Application.Extensions;
using E_CommerceApp.Api.Application.Interfaces.Repositories;
using E_CommerceApp.Infrastructure.Persistence.Context;
using E_CommerceApp.Infrastructure.Persistence.Extensions;
using E_CommerceApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddFluentValidation();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.AddRepositoriesRegistration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
