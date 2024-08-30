using System.ComponentModel.DataAnnotations;

namespace Minimal_API.Dominio.DTOs.Veiculo;

public class VeiculoDTO
{
    public string Nome { get; set; }
    public string Marca { get; set; }
    public int AnoFabricacao { get; set; } = default!;
}
