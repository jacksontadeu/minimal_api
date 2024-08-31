using Minimal_API.Dominio.DTOs.Veiculo;

namespace Minimal_API.Dominio.Validacoes;

public class VeiculoValidacao
{
    public MensagemErro ValidaDTO(VeiculoDTO veiculoDTO)
    {
        var validacao = new MensagemErro
        {
            Mensagens = new List<string>()
        };

        if (string.IsNullOrEmpty(veiculoDTO.Nome))
            validacao.Mensagens.Add("O nome do veículo deve ser válido e não em branco");

        if (string.IsNullOrEmpty(veiculoDTO.Marca))
            validacao.Mensagens.Add("A marca deve ser válida e não em branco");

        DateTime ano = DateTime.Now;
        if (veiculoDTO.AnoFabricacao < 1950 || veiculoDTO.AnoFabricacao > ano.Year)
            validacao.Mensagens.Add("O ano nao deve ser menor que 1950 e maior que o ano atual");

        return validacao;
    }
}
