using AutoMapper;
using CadastroClientes.Models;
using CadastroClientes.Repositorio;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ClienteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddSingleton<AutoMapper.IConfigurationProvider>(sp =>
{
    return new MapperConfiguration(cfg =>
    {
        cfg.AddMaps(typeof(Program).Assembly);
    });
});
builder.Services.AddScoped<IMapper>(sp =>
    new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

builder.Services.AddScoped<IRepositorio<Cliente>, RepositorioBase<Cliente>>();
builder.Services.AddScoped<ClienteRepositorio>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cliente}/{action=Index}/{id?}");

app.Run();