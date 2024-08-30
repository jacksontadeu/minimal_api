using Microsoft.EntityFrameworkCore;
using Minimal_API.Dominio.DTOs.Veiculo;
using Minimal_API.Dominio.Entidades;
using Minimal_API.Dominio.Interfaces;
using Minimal_API.Infraestrutura.Data;

namespace Minimal_API.Dominio.Servicos;

public class VeiculoServico : IVeiculoServico
{
    private readonly DbContexto _context;

    public VeiculoServico(DbContexto context)
    {
        _context = context;
    }

    public List<Veiculo> ListarTodos()
    {
        return _context.Veiculos.ToList();
    }
    public Veiculo ListarPorId(int id)
    {
        var veiculo = _context.Veiculos.FirstOrDefault(v => v.Id == id);
        return veiculo;
    }
    public void IncluirVeiculo(Veiculo veiculo)
    {
        _context.Veiculos.Add(veiculo);
        _context.SaveChanges();
    }
    public void AlterarVeiculo(Veiculo veiculo)
    {
        //_context.Update(veiculo);
        //_context.SaveChanges();
        _context.Entry(veiculo).State = EntityState.Modified;
        _context.SaveChanges();
        
    }
    public void DeletarVeiculo(Veiculo veiculo)
    {  
        _context.Veiculos.Remove(veiculo);
        _context.SaveChanges();
    }
}
