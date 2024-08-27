using Microsoft.EntityFrameworkCore;
using Minimal_API.Dominio.Entidades;

namespace Minimal_API.Infraestrutura.Data;

public class DbContexto : DbContext{
    public DbContexto(DbContextOptions options) : base(options)
    { }

    public DbSet<Administrador> Administradores { get; set; }

   

}
