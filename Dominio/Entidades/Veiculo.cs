using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minimal_API.Dominio.Entidades;

public class Veiculo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{ get; set; }
    [Required]
    [StringLength(100)]
    public string Nome { get; set; }
    [Required]
    [StringLength(50)]
    public string Marca { get; set; }
    [Required]
    [StringLength(4)]
    public int AnoFabricacao { get; set; } = default!;
}
