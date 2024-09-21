using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class TrabajoService
{
	private readonly Contexto _contexto;

	public TrabajoService(Contexto contexto)
	{
		_contexto = contexto;
	}

	public async Task<bool> Guardar(Trabajo trabajo)
	{
		if (!await Existe(trabajo.TrabajoId))
			return await Insertar(trabajo);
		else
			return await Modificar(trabajo);
	}

	private async Task<bool> Existe(int trabajoId)
	{
		return await _contexto.Trabajo
			.AnyAsync(t => t.TrabajoId == trabajoId);
	}

	public async Task<bool> ExisteCliente(int clienteId, string nombre)
	{
		return await _contexto.Clientes
			.AnyAsync(c => c.ClienteId != clienteId
			&& c.Nombres.ToLower().Equals(nombre.ToLower()));
	}



	private async Task<bool> Insertar(Trabajo trabajo)
	{
		_contexto.Trabajo
			.Add(trabajo);
		return await _contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Trabajo trabajo)
	{
		_contexto.Update(trabajo);
		var modificado = await _contexto.SaveChangesAsync() > 0;
		_contexto.Entry(trabajo).State = EntityState.Detached;
		return modificado;
	}
	public async Task<bool> Eliminar(Trabajo trabajo)
	{
		return await _contexto.Trabajo
			.AsNoTracking()
			.Where(t => t.TrabajoId == trabajo.TrabajoId)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<Trabajo?> BuscarId(int id)
	{
		return await _contexto.Trabajo
			.AsNoTracking()
			.FirstOrDefaultAsync(t => t.TrabajoId == id);
	
	}

	public async Task<List<Trabajo>> Listar(Expression<Func<Trabajo, bool>> criterio)
	{
		return await _contexto.Trabajo
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();

	}

}
