using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class ClientesService
{
	private readonly Contexto _contexto;

	public ClientesService(Contexto contexto)
	{
		_contexto = contexto;
	}

	public async Task<bool> Guardar(Clientes cliente)
	{
		if (!await Existe(cliente.ClienteId))
			return await Insertar(cliente);
		else
			return await Modificar(cliente);
	}

	private async Task<bool> Existe(int clienteId)
	{
		return await _contexto.Clientes
			.AnyAsync(c => c.ClienteId == clienteId);
	}

	public async Task<bool> ExisteCliente(int clienteId, string nombre)
	{
		return await _contexto.Clientes
			.AnyAsync(c => c.ClienteId != clienteId
			&& c.Nombres.ToLower().Equals(nombre.ToLower()));
	}



	private async Task<bool> Insertar(Clientes cliente)
	{
		_contexto.Clientes
			.Add(cliente);
		return await _contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Clientes cliente)
	{
		_contexto.Update(cliente);
		var modificado = await _contexto.SaveChangesAsync() > 0;
		_contexto.Entry(cliente).State = EntityState.Detached;
		return modificado;
	}
	public async Task<bool> Eliminar(Clientes cliente)
	{
		return await _contexto.Clientes
			.AsNoTracking()
			.Where(c => c.ClienteId == cliente.ClienteId)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<Clientes?> BuscarId(int id)
	{
		return await _contexto.Clientes
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.ClienteId == id);
	}

	public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
	{
		return await _contexto.Clientes
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();

	}

}
