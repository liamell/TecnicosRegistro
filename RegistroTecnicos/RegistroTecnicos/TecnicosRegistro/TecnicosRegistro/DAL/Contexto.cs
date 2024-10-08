﻿using Microsoft.EntityFrameworkCore;
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

}

