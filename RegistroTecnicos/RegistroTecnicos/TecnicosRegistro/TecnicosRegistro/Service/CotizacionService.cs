using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class CotizacionService(IDbContextFactory<Contexto> DbFactory)
{
        private async Task<bool> Existe(int cotizacionId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cotizar.AnyAsync(c => c.CotizacionId == cotizacionId);
        }

        private async Task<bool> Insertar(Cotizar cotizar)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Cotizar.Add(cotizar);
                return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Cotizar cotizar)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Cotizar.Update(cotizar);
            var modificado = await contexto.SaveChangesAsync() > 0;
                return modificado;
        }

        public async Task<bool> Guardar(Cotizar cotizar)
        {
            if (!await Existe(cotizar.CotizacionId))
                return await Insertar(cotizar);
            else
                return await Modificar(cotizar);
        }

        public async Task<bool> Eliminar(int cotizacionId)
        {

            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cotizar
            .Include(d => d.cotizacionesDetalle)
            .Where(c => c.CotizacionId == cotizacionId)
             .ExecuteDeleteAsync() > 0;
        }

        public async Task<Cotizar> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cotizar
            .Include(d => d.cotizacionesDetalle)
            .FirstOrDefaultAsync(c => c.CotizacionId == id);
        }
        public async Task<Cotizar> BuscarConDetalles(int Id)
        {

            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cotizar
            .Include(t => t.Cliente)
             .Include(t => t.cotizacionesDetalle)
             .ThenInclude(td => td.Articulo)
             .FirstOrDefaultAsync(t => t.CotizacionId == Id);
        }
        public async Task<List<Cotizar>> Listar(Expression<Func<Cotizar, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cotizar
                .Include(c => c.Cliente)
                .Include(d => d.cotizacionesDetalle)
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
}
