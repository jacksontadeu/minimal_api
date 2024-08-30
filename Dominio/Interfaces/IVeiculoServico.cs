using Minimal_API.Dominio.DTOs.Veiculo;
using Minimal_API.Dominio.Entidades;

namespace Minimal_API.Dominio.Interfaces;

public interface IVeiculoServico
{
    void IncluirVeiculo(Veiculo veiculo);
    List<Veiculo> ListarTodos();
    Veiculo ListarPorId(int id);
    void AlterarVeiculo(Veiculo veiculo);
    void DeletarVeiculo(Veiculo veiculo);

}
