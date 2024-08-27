using Minimal_API.Dominio.DTOs;
using Minimal_API.Dominio.Entidades;
using Minimal_API.Dominio.Interfaces;
using Minimal_API.Infraestrutura.Data;

namespace Minimal_API.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
    private readonly DbContexto _contexto;

    public AdministradorServico(DbContexto contexto)
    {
        _contexto = contexto;
    }

    public Administrador? Login(LoginDTO loginDTO)
    {
        var usuario = _contexto.Administradores.Where(a => a.Email.Equals(loginDTO.Email) &&
                     a.Senha.Equals(loginDTO.Senha)).FirstOrDefault();
        return usuario;
           
    }
}
