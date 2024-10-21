using Microsoft.EntityFrameworkCore;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.DAL;

public class Contexto : DbContext
{
	public Contexto(DbContextOptions<Contexto> options) : base(options)
	{

	}

	public DbSet<Tecnicos> Tecnicos { get; set; }
	public DbSet<TipoTecnico> TipoTecnico { get; set; }

	public DbSet<Clientes> Clientes { get; set; }

	public DbSet<Trabajo> Trabajo { get; set; }

	public DbSet<Prioridad> Prioridad { get; set; }

	public DbSet<Articulo> Articulo { get; set; }

	public DbSet<TrabajosDetalle> TrabajosDetalle { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Articulo>().HasData(new List<Articulo>()
		{
			new Articulo(){ArticuloId=1, Descripcion= "Cable tipo C", Costo= 100, Precio= 165, Existencia= 30},
			new Articulo(){ArticuloId=2, Descripcion="Cable hdmi", Costo= 300, Precio= 500, Existencia= 20},
			new Articulo(){ArticuloId=3, Descripcion="Memoria USB 10gb", Costo= 300, Precio= 450, Existencia= 50},
			

		});
	}



}

