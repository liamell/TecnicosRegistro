using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class TipoTecnicoService
{
		private readonly Contexto _contexto;

	public TipoTecnicoService(Contexto contexto)
	{
		_contexto = contexto;

	}

	public async Task<bool> Guardar(TipoTecnico tipoTecnico)
	{
		if (!await Existe(tipoTecnico.TipoId))
			return await Insertar(tipoTecnico);
		else
			return await Modificar(tipoTecnico);

	}

	private async Task<bool> Modificar(TipoTecnico tipoTecnico)
	{
		_contexto.Update(tipoTecnico);
		var modificado = await _contexto.SaveChangesAsync() > 0;
		_contexto.Entry(tipoTecnico).State = EntityState.Detached;
		return modificado;

			
	}

	private async Task<bool> Insertar(TipoTecnico tipoTecnico)
	{
		_contexto.TipoTecnico.Add(tipoTecnico);
		return await _contexto.SaveChangesAsync() > 0;


	}

	private async Task<bool> Existe(int tipoId)
	{
		return await _contexto.TipoTecnico
			.AnyAsync(tp => tp.TipoId == tipoId);

	}

	public async Task<bool> ExisteDescripcion(int tipoId, string descripcion)
	{
		return await _contexto.TipoTecnico
			.AnyAsync(tp => tp.TipoId != tipoId
			&& tp.Descripcion.ToLower().Equals(descripcion.ToLower()));
	}

	public async Task<bool> Eliminar(TipoTecnico tipoTecnico)
	{
		return await _contexto.TipoTecnico
			.AsNoTracking()
			.Where(tp => tp.TipoId == tipoTecnico.TipoId)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<TipoTecnico?> BuscarId(int id)
	{
		return await _contexto.TipoTecnico
			.AsNoTracking()
			.FirstOrDefaultAsync(tp => tp.TipoId == id);
	}

	public async Task<List<TipoTecnico>> Listar(Expression<Func<TipoTecnico, bool>> criterio)
	{
		return await _contexto.TipoTecnico
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}
	