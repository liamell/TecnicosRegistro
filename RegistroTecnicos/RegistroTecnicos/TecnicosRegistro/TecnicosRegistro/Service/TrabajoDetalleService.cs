using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class TrabajoDetalleService(Contexto contexto)
{
	public async Task<List<Articulo>> Listar(Expression<Func<Articulo, bool>> criterio)
	{
		return await contexto
			.Articulo
			.Where(criterio)
			.ToListAsync();
	}
}
