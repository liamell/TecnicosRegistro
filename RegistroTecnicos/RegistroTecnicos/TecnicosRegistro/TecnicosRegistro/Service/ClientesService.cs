using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class ClientesService(IDbContextFactory<Contexto> DbFactory)
{
	
	public async Task<bool> Guardar(Clientes cliente)
	{
		if (!await Existe(cliente.ClienteId))
			return await Insertar(cliente);
		else
			return await Modificar(cliente);
	}

	private async Task<bool> Existe(int clienteId)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
			.AnyAsync(c => c.ClienteId == clienteId);
	}

	public async Task<bool> ExisteCliente(int clienteId, string nombre)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
			.AnyAsync(c => c.ClienteId != clienteId
			&& c.Nombres.ToLower().Equals(nombre.ToLower()));
	}



	private async Task<bool> Insertar(Clientes cliente)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Clientes
			.Add(cliente);
		return await contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Clientes cliente)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(cliente);
		var modificado = await contexto.SaveChangesAsync() > 0;
		contexto.Entry(cliente).State = EntityState.Detached;
		return modificado;
	}
	public async Task<bool> Eliminar(Clientes cliente)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
			.AsNoTracking()
			.Where(c => c.ClienteId == cliente.ClienteId)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<Clientes?> BuscarId(int id)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.ClienteId == id);
	}

	public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();

	}

}
