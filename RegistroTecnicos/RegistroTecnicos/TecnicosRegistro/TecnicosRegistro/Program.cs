using Microsoft.EntityFrameworkCore;
using TecnicosRegistro.Components;
using TecnicosRegistro.DAL;
using TecnicosRegistro.Models;
using TecnicosRegistro.Service;
using TecnicosRegistro.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBlazorBootstrap();

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var ConStr = builder.Configuration.GetConnectionString("ConStr");
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlite(ConStr));

var ConStrTipoTecnico = builder.Configuration.GetConnectionString("ConStrTipoTecnico");

builder.Services.AddScoped<TecnicoService>();
builder.Services.AddScoped<TipoTecnicoService>();
builder.Services.AddScoped<ClientesService>();
builder.Services.AddScoped<TrabajoService>();
builder.Services.AddScoped<PrioridadService>();
builder.Services.AddScoped<ArticuloService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
