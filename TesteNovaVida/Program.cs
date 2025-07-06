using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using TesteNovaVida;
using TesteNovaVida.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TesteNovaVidaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TesteNovaVidaContext") ?? throw new InvalidOperationException("Connection string 'TesteNovaVidaContext' not found.")));



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


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
   app.UseRequestLocalization(); // Add this line

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
