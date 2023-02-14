
using ControleUsuario.Infra.Data.Interfaces;
using ControleUsuario.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
// Configurando o projeto para MVC
builder.Services.AddControllersWithViews();
//habilitando o uso de sessões no projeto
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//habilitando o projeto para usar permissões de acesso
builder.Services.Configure<CookiePolicyOptions>(options => {
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
//capturar a connectionstring mapeada no 'appsettings.json'
var connectionString =
builder.Configuration.GetConnectionString("UsuarioWeb");
//injeção de dependencia para as classes do repositório
//enviar a connectionstring para a classe UsuarioRepository
builder.Services.AddTransient<IUsuarioRepository>
 (map => new UsuarioRepository(connectionString));
var app = builder.Build();
//habilitando o uso de sessões no projeto
app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseRouting();
//autenticação e autorização
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
// Definindo a página inicial do projeto
app.MapControllerRoute(
 name: "default",
 pattern: "{controller=Account}/{action=Index}"
 );
app.Run();
