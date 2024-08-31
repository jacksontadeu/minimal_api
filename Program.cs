using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minimal_API.Dominio.DTOs;
using Minimal_API.Dominio.DTOs.Veiculo;
using Minimal_API.Dominio.Entidades;
using Minimal_API.Dominio.Interfaces;
using Minimal_API.Dominio.Servicos;
using Minimal_API.Dominio.Validacoes;
using Minimal_API.Infraestrutura.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var conexao = builder.Configuration.GetConnectionString("mysql");

builder.Services.AddDbContext<DbContexto>(options =>
    options.UseMySql(conexao, ServerVersion.AutoDetect(conexao)));

builder.Services.AddScoped<IAdministradorServico,AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico,VeiculoServico>();

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

app.MapGet("veiculo/listartodos", (IVeiculoServico veiculoServico) =>
{
    return veiculoServico.ListarTodos();
});

app.MapGet("veiculo/buscarporid/{id}", (IVeiculoServico veiculoServico, [FromRoute]int id) =>
{
    var veiculo = veiculoServico.ListarPorId(id);
    if(veiculo == null) return Results.NotFound();
    return Results.Ok(veiculo);
    
});

//app.MapPut("veiculo/alterarveiculo/{id}", ([FromRoute]int id,IVeiculoServico veiculoServico, VeiculoDTO veiculoDTO) =>
//{
//    var veiculo = veiculoServico.ListarPorId(id);
//    if (veiculo == null) return Results.NotFound();

//    veiculo.Nome = veiculoDTO.Nome;
//    veiculo.Marca = veiculoDTO.Marca;
//    veiculo.AnoFabricacao = veiculoDTO.AnoFabricacao;

//    veiculoServico.AlterarVeiculo(veiculo);
//    return Results.Ok(veiculo);
//});
app.MapPut("veiculo/alterarveiculo", (Veiculo? veiculo, IVeiculoServico veiculoservico) =>
{
    if (veiculo == null)
        return Results.NotFound();
    veiculoservico.AlterarVeiculo(veiculo);
    return Results.Ok(veiculo);
});

app.MapDelete("veiculo/deletarveiculo/{id}", ([FromRoute] int id,IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.ListarPorId(id);
    if (veiculo == null) return Results.NotFound();
    veiculoServico.DeletarVeiculo(veiculo);
    return Results.Ok("Deletado com sucesso");
});

app.MapPost("veiculo/IncluirVeiculo", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    var validacao = new VeiculoValidacao();
    var valida = validacao.ValidaDTO(veiculoDTO);

    if (valida.Mensagens.Count > 0)
        return Results.BadRequest(valida);

    var veiculo = new Veiculo
    {
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        AnoFabricacao = veiculoDTO.AnoFabricacao
    };
    veiculoServico.IncluirVeiculo(veiculo);
    return Results.Created(string.Empty, veiculo);
});

app.MapPost("/login", ([FromBody]LoginDTO loginDTO, IAdministradorServico administradorServico) =>
{
    if (administradorServico.Login(loginDTO) != null)
        return Results.Ok("Login realizado com sucesso!!!");
    else return Results.Unauthorized();

});

app.Run();

