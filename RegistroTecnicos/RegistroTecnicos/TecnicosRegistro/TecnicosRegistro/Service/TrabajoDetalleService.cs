using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class TrabajoDetalleService(IDbContextFactory<Contexto> DbFactory)
{
	public async Task<List<Articulo>> Listar(Expression<Func<Articulo, bool>> criterio)
	{
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto
			.Articulo
			.Where(criterio)
			.ToListAsync();
	}
}
