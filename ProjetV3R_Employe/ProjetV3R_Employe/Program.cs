using ProjetV3R_Employe.Data.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using ProjetV3R_Employe.Data.Services;


var builder = WebApplication.CreateBuilder(args);

try
{
    Console.WriteLine("Chargement des param�tres depuis appsettings.json...");
    var config = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
    Console.WriteLine($"Connexion : {config}");
}
catch (Exception ex)
{
    Console.WriteLine($"Erreur lors du chargement : {ex.Message}");
}

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor(); 
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<EmployeService>();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthenticationStateProvider>());





// Configuration de la base de donnees avec MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 18))));



// Configuration de l'authentification avec cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.None; // en local chakal
        //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Utilise HTTPS
        options.Cookie.SameSite = SameSiteMode.None; // Permet la transmission dans toutes les situations
        Console.WriteLine("Authentification par cookies configur�e.");
    });



// Ajouter les services d'autorisation avec des politiques de r�le
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Administrateur"));
});


builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"] ?? "https://localhost:7141/");
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));


// Ajout de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policyBuilder => policyBuilder
            .WithOrigins("https://localhost:7141") // Remplacez par l'URL de votre frontend
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.Use(async (context, next) =>
{
    await next();

    var setCookieHeader = context.Response.Headers["Set-Cookie"];
    Console.WriteLine($"Set-Cookie Header: {setCookieHeader}");
});

app.Use(async (context, next) =>
{
    var cookies = context.Request.Headers["Cookie"];
    Console.WriteLine($"Cookies transmis : {cookies}");
    await next();
});

app.Use(async (context, next) =>
{
    var cookies = context.Request.Headers["Cookie"];
    Console.WriteLine($"[SignalR] Cookies transmis : {cookies}");
    await next();
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activation de CORS
app.UseCors("AllowBlazor");

// Activer l'authentification et l'autorisation
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub()
        .RequireAuthorization(); // Requiert l'authentification pour toutes les connexions SignalR
    endpoints.MapFallbackToPage("/_Host");
});

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();