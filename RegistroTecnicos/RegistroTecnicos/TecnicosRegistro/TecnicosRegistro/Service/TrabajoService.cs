using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;

namespace TecnicosRegistro.Service;

public class TrabajoService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int trabajoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajo.AnyAsync(e => e.TrabajoId == trabajoId);
    }

    private async Task<bool> Insertar(Trabajo trabajo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        await AfectarCantidad(trabajo.TrabajosDetalle.ToArray(), true);
        contexto.Trabajo.Add(trabajo);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Trabajo trabajos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var trabajoOriginal = await contexto.Trabajo
        .Include(t => t.TrabajosDetalle)
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.TrabajoId == trabajos.TrabajoId);

        await AfectarCantidad(trabajoOriginal.TrabajosDetalle.ToArray(), false);

        await AfectarCantidad(trabajos.TrabajosDetalle.ToArray(), true);

        contexto.Update(trabajos);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Trabajo trabajo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (!await Existe(trabajo.TrabajoId))
            return await Insertar(trabajo);
        else
            return await Modificar(trabajo);
    }

    public async Task<bool> Eliminar(int trabajoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var trabajo = contexto.Trabajo.Find(trabajoId);
        if (trabajo == null)
            return false;

        await AfectarCantidad(trabajo.TrabajosDetalle.ToArray(), false);
        return await contexto.Trabajo
            .Include(t => t.TrabajosDetalle)
            .Where(e => e.TrabajoId == trabajoId) 
            .ExecuteDeleteAsync() > 0;
    }
    public async Task<Trabajo> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajo
            .Include(e => e.Tecnico).Include(e => e.Cliente)
            .Include(e => e.Prioridad)
            .Include(t => t.TrabajosDetalle)
            .AsNoTracking()
           .FirstOrDefaultAsync(e => e.TrabajoId == id);
    }

    public async Task<Trabajo> BuscarConDetalles(int trabajoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajo
            .Include(t => t.Prioridad)
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .Include(t => t.TrabajosDetalle)
            .ThenInclude(td => td.Articulo)
            .FirstOrDefaultAsync(t => t.TrabajoId == trabajoId);
    }

    public async Task<List<Trabajo>> Listar(Expression<Func<Trabajo, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajo.Include(e => e.Tecnico)
            .Include(e => e.Cliente)
            .Include(e => e.Prioridad)
            .Include(t => t.TrabajosDetalle)
            .AsNoTracking().Where(criterio).ToListAsync();
    }

    public async Task<bool> ExisteTrabajo(int trabajoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await
            contexto.Trabajo
            .AnyAsync(e => e.TrabajoId == trabajoId);
    }

    public async Task AfectarCantidad(TrabajosDetalle[] detalles, bool resta)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var item in detalles)
        {
            var articulo = await contexto.Articulo.SingleAsync(a => a.ArticuloId == item.ArticuloId);
            if (resta)
                articulo.Existencia -= item.Cantidad;
            else
                articulo.Existencia += item.Cantidad;
        }
        await contexto.SaveChangesAsync(); // Guarda los cambios
    }

}


