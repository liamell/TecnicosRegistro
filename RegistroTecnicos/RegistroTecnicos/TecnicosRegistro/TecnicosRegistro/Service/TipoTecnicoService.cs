using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class TipoTecnicoService(IDbContextFactory<Contexto> DbFactory)
{
	
	public async Task<bool> Guardar(TipoTecnico tipoTecnico)
	{
		if (!await Existe(tipoTecnico.TipoId))
			return await Insertar(tipoTecnico);
		else
			return await Modificar(tipoTecnico);

	}

	private async Task<bool> Modificar(TipoTecnico tipoTecnico)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(tipoTecnico);
		var modificado = await contexto.SaveChangesAsync() > 0;
		contexto.Entry(tipoTecnico).State = EntityState.Detached;
		return modificado;

			
	}

	private async Task<bool> Insertar(TipoTecnico tipoTecnico)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.TipoTecnico.Add(tipoTecnico);
		return await contexto.SaveChangesAsync() > 0;


	}

	private async Task<bool> Existe(int tipoId)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TipoTecnico
			.AnyAsync(tp => tp.TipoId == tipoId);

	}

	public async Task<bool> ExisteDescripcion(int tipoId, string descripcion)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TipoTecnico
			.AnyAsync(tp => tp.TipoId != tipoId
			&& tp.Descripcion.ToLower().Equals(descripcion.ToLower()));
	}

	public async Task<bool> Eliminar(TipoTecnico tipoTecnico)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TipoTecnico
			.AsNoTracking()
			.Where(tp => tp.TipoId == tipoTecnico.TipoId)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<TipoTecnico?> BuscarId(int id)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TipoTecnico
			.AsNoTracking()
			.FirstOrDefaultAsync(tp => tp.TipoId == id);
	}

	public async Task<List<TipoTecnico>> Listar(Expression<Func<TipoTecnico, bool>> criterio)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TipoTecnico
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}
	