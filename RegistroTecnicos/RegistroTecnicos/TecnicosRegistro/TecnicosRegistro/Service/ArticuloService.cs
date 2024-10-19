using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;
namespace TecnicosRegistro.Service;

public class ArticuloService(Contexto contexto)
{
	public async Task<bool> Guardar(Articulo articulo)
	{
		if (!await Existe(articulo.ArticuloId))
			return await Insertar(articulo);
		else
			return await Modificar(articulo);
	}

	
	private async Task<bool> Existe(int articuloId)
	{
		return await contexto.Articulo.AnyAsync(a => a.ArticuloId == articuloId);
	}


	private async Task<bool> Insertar(Articulo articulo)
	{
		contexto.Articulo.Add(articulo);
		return await contexto.SaveChangesAsync() > 0;
	}


	private async Task<bool> Modificar(Articulo articulo)
	{
		contexto.Update(articulo);
		var modificado = await contexto.SaveChangesAsync() > 0;
		contexto.Entry(articulo).State = EntityState.Modified;
		return modificado;
	}

	public async Task<bool> Eliminar(int articuloId)
	{
		return await contexto.Articulo
			.AsNoTracking()
			.Where(a => a.ArticuloId == articuloId)
		.ExecuteDeleteAsync() > 0;
	}

	
	public async Task<Articulo?> Buscar(int articuloId)
	{
		return await contexto.Articulo
			.AsNoTracking()
			.FirstOrDefaultAsync(a => a.ArticuloId == articuloId);
	}

	public async Task<List<Articulo>> Listar(Expression<Func<Articulo, bool>> filtro)
	{
		return await contexto.Articulo
			.Where(filtro)
			.ToListAsync();
	}

}
