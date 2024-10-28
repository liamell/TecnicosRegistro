using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class TecnicoService(IDbContextFactory<Contexto> DbFactory)
{
	

	public async Task<bool> Guardar(Tecnicos tecnico)
	{
		if (!await Existe(tecnico.TecnicoId))
			return await Insertar(tecnico);
		else
			return await Modificar(tecnico);
	}

	private async Task<bool> Existe(int tecnicoId)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
			.AnyAsync(t => t.TecnicoId == tecnicoId);
	}

	public async Task<bool> ExisteTecnico(int tecnicoId, string nombre)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
			.AnyAsync(t => t.TecnicoId != tecnicoId
			&& t.Nombres.ToLower().Equals(nombre.ToLower()));
	}

 



    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tecnicos.Add(tecnico);
		return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(tecnico);
		var modificado = await contexto.SaveChangesAsync() > 0;
		contexto.Entry(tecnico).State = EntityState.Detached;
		return modificado;
	}
	public async Task<bool> Eliminar(Tecnicos tecnico)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
			.AsNoTracking()
			.Where(t => t.TecnicoId == tecnico.TecnicoId)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<Tecnicos?> BuscarId(int id)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
			.AsNoTracking()
			.FirstOrDefaultAsync(t => t.TecnicoId == id);
	}

	public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();

	}
}
