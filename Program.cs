using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minimal_API.Dominio.DTOs;
using Minimal_API.Dominio.Interfaces;
using Minimal_API.Dominio.Servicos;
using Minimal_API.Infraestrutura.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var conexao = builder.Configuration.GetConnectionString("mysql");

builder.Services.AddDbContext<DbContexto>(options =>
    options.UseMySql(conexao, ServerVersion.AutoDetect(conexao)));

builder.Services.AddScoped<IAdministradorServico,AdministradorServico>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/login", ([FromBody]LoginDTO loginDTO, IAdministradorServico administradorServico) =>
{
    if (administradorServico.Login(loginDTO) != null)
        return Results.Ok("Login realizado com sucesso!!!");
    else return Results.Unauthorized();

});

app.Run();

