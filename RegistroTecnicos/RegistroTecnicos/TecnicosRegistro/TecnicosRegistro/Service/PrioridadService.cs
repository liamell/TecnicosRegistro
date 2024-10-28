using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Services
{
    public class PrioridadService(IDbContextFactory<Contexto> DbFactory)
    {
       
        public async Task<bool> Guardar(Prioridad prioridad)
        {
          
            if (!await Existe(prioridad.PrioridadId))
                return await Insertar(prioridad);
            else
                return await Modificar(prioridad);
        }

        public async Task<bool> Insertar(Prioridad prioridad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Prioridad.Add(prioridad);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Prioridad prioridad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(prioridad);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Existe(int PrioridadId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Prioridad
                .AnyAsync(p => p.PrioridadId == PrioridadId);
        }

        public async Task<bool> Eliminar(Prioridad prioridad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Prioridad
                .AsNoTracking()
                .Where(p => p.PrioridadId == prioridad.PrioridadId)
                .ExecuteDeleteAsync() > 0;

        }

        public async Task<Prioridad?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Prioridad
                .AsNoTracking()
                .FirstOrDefaultAsync(P => P.PrioridadId == id);
        }

        public async Task<List<Prioridad>> Listar(Expression<Func<Prioridad, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Prioridad
                                 .AsNoTracking()
                                 .Where(criterio)
                                 .ToListAsync();
        }

    }
}
