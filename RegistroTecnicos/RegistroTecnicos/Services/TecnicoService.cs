using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicoService
{
	private readonly Contexto _contexto;

	public TecnicoService(Contexto contexto)
	{
		_contexto = contexto;
	}

	public async Task<bool> Guardar(Tecnicos tecnico)
	{
		if (!await Existe(tecnico.TecnicoId))
			return await Insertar(tecnico);
		else
			return await Modificar(tecnico);
	}

	private async Task<bool> Existe(int tecnicoId)
	{
		return await _contexto.Tecnicos
			.AnyAsync(t => t.TecnicoId == tecnicoId);
	}

	public async Task<bool> ExisteTecnico(int tecnicoId, string nombre)
	{
		return await _contexto.Tecnicos
			.AnyAsync(t => t.TecnicoId != tecnicoId
			&& t.Nombres.ToLower().Equals(nombre.ToLower()));
	}

	

	private async Task<bool> Insertar(Tecnicos tecnico)
	{
		_contexto.Tecnicos.Add(tecnico);
		return await _contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Tecnicos tecnico)
	{
		_contexto.Update(tecnico);
		var modificado = await _contexto.SaveChangesAsync() > 0;
		_contexto.Entry(tecnico).State = EntityState.Detached;
		return modificado;
	}
	public async Task<bool> Eliminar(Tecnicos tecnico)
	{
		return await _contexto.Tecnicos
			.AsNoTracking()
			.Where(t => t.TecnicoId == tecnico.TecnicoId)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<Tecnicos?> BuscarId(int id)
	{
		return await _contexto.Tecnicos
			.AsNoTracking()
			.FirstOrDefaultAsync(t => t.TecnicoId == id);
	}

	public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
	{
		return await _contexto.Tecnicos
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();

	}

}
