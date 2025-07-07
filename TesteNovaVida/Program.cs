using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TesteNovaVida.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TesteNovaVidaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TesteNovaVidaContext") ?? throw new InvalidOperationException("Connection string 'TesteNovaVidaContext' not found.")));


//Setando as configurações de região: Brasil 
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultureInfo = new CultureInfo("pt-BR");
    cultureInfo.NumberFormat.CurrencySymbol = "R$";

    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

    var supportedCultures = new[] { "pt-BR" }; // Brazilian Portuguese
    options.DefaultRequestCulture = new RequestCulture("pt-BR");

    options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
    {
    // My custom request culture logic
    return await Task.FromResult(new ProviderCultureResult("pt-BR"));
    }));
});

////Uso de Sessão
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromSeconds(10);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "Professor",
    pattern: "{controller=Professors}/{action=Index}/{id?}").WithStaticAssets();

app.Run();
