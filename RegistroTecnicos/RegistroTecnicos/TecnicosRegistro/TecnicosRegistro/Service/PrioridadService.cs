using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Services
{
    public class PrioridadService
    {
        private readonly Contexto _context;
        public PrioridadService(Contexto contexto)
        {
            _context = contexto;
        }

        public async Task<bool> Guardar(Prioridad prioridad)
        {
          
            if (!await Existe(prioridad.PrioridadId))
                return await Insertar(prioridad);
            else
                return await Modificar(prioridad);
        }

        public async Task<bool> Insertar(Prioridad prioridad)
        {
            _context.Prioridad.Add(prioridad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Prioridad prioridad)
        {
            _context.Update(prioridad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Existe(int PrioridadId)
        {
            return await _context.Prioridad
                .AnyAsync(p => p.PrioridadId == PrioridadId);
        }

        public async Task<bool> Eliminar(Prioridad prioridad)
        {

            return await _context.Prioridad
                .AsNoTracking()
                .Where(p => p.PrioridadId == prioridad.PrioridadId)
                .ExecuteDeleteAsync() > 0;

        }

        public async Task<Prioridad?> Buscar(int id)
        {
            return await _context.Prioridad
                .AsNoTracking()
                .FirstOrDefaultAsync(P => P.PrioridadId == id);
        }

        public async Task<List<Prioridad>> Listar(Expression<Func<Prioridad, bool>> criterio)
        {
            return await _context.Prioridad
                                 .AsNoTracking()
                                 .Where(criterio)
                                 .ToListAsync();
        }

    }
}
