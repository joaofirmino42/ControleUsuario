
using ControleUsuario.Infra.Data.Interfaces;
using ControleUsuario.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
// Configurando o projeto para MVC
builder.Services.AddControllersWithViews();
//habilitando o uso de sess�es no projeto
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//habilitando o projeto para usar permiss�es de acesso
builder.Services.Configure<CookiePolicyOptions>(options => {
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
//capturar a connectionstring mapeada no 'appsettings.json'
var connectionString =
builder.Configuration.GetConnectionString("UsuarioWeb");
//inje��o de dependencia para as classes do reposit�rio
//enviar a connectionstring para a classe UsuarioRepository
builder.Services.AddTransient<IUsuarioRepository>
 (map => new UsuarioRepository(connectionString));
var app = builder.Build();
//habilitando o uso de sess�es no projeto
app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseRouting();
//autentica��o e autoriza��o
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
// Definindo a p�gina inicial do projeto
app.MapControllerRoute(
 name: "default",
 pattern: "{controller=Account}/{action=Index}"
 );
app.Run();
