using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class CotizacionDetalleService(IDbContextFactory<Contexto> DbFactory)
{

    public async Task<Articulo> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Articulo.AsNoTracking().FirstOrDefaultAsync(p => p.ArticuloId == id);
    }
    public async Task<List<Articulo>> Listar(Expression<Func<Articulo, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Articulo.Where(criterio).ToListAsync();
    }
}
