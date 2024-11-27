using ProjetV3R_Employe.Data.Models;
using ProjetV3R_Employe.Data.Models.ProjetV3R;
using ProjetV3R_Employe.SignalR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

try
{
    Console.WriteLine("Chargement des paramètres depuis appsettings.json...");
    var config = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
    Console.WriteLine($"Connexion : {config}");
}
catch (Exception ex)
{
    Console.WriteLine($"Erreur lors du chargement de la BD Employes: {ex.Message}");
}


try
{
    Console.WriteLine("Chargement des paramètres depuis appsettings.json...");
    var config2 = builder.Configuration.GetSection("ConnectionStrings:FournisseursConnection").Value;
    Console.WriteLine($"Connexion : {config2}");
}
catch (Exception ex)
{
    Console.WriteLine($"Erreur lors du chargement de la BD Fournisseurs: {ex.Message}");
}

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor(); 
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<EmployeService>();
builder.Services.AddScoped<FournisseurService>();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthenticationStateProvider>());


builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7141");
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        UseCookies = true,
        CookieContainer = new CookieContainer()
    };
});



// Configuration de la BD Employe
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 18))));

// Configuration de la base de donnees Fournisseur
builder.Services.AddDbContext<ApplicationDbContext2>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("FournisseursConnection"),
    new MySqlServerVersion(new Version(8, 0, 18))));



// Configuration de l'authentification avec cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1); // Durée de vie du cookie
        options.SlidingExpiration = true; // Renouvelle le cookie automatiquement
        //options.Cookie.HttpOnly = true; // Empêche l'accès via JavaScript
        options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Pour développement
        options.Cookie.SameSite = SameSiteMode.Lax; // Compatibilité navigateur
        //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Utilise HTTPS
        Console.WriteLine("Authentification par cookies configurée.");
    });



// Ajouter les services d'autorisation avec des politiques de rôle
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Administrateur"));
    options.AddPolicy("Commis", policy => policy.RequireRole("Commis"));
    options.AddPolicy("Employe", policy => policy.RequireRole("Employé"));
    options.AddPolicy("AdministrateurGeneralOnly", policy => policy.RequireRole("Administrateur Général"));
    options.AddPolicy("ResponsableOnly", policy => policy.RequireRole("Responsable"));
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activation de CORS
app.UseCors("AllowBlazor");

// Activer l'authentification et l'autorisation
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();


app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("Cache-Control");
    context.Response.Headers.Remove("Pragma");
    await next();
});

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
    Console.WriteLine($"[Debug Middleware] Cookies dans la requête : {cookies}");

    await next();

    var setCookieHeader = context.Response.Headers["Set-Cookie"];
    Console.WriteLine($"[Debug Middleware] Set-Cookie dans la réponse : {setCookieHeader}");
});

app.Use(async (context, next) =>
{
    var user = context.User;
    if (user?.Identity?.IsAuthenticated == true)
    {
        Console.WriteLine($"Utilisateur connecté : {user.Identity.Name}");
    }
    else
    {
        Console.WriteLine("Aucun utilisateur connecté.");
    }

    await next();
});

app.MapControllers();
app.MapHub<ConnectionHub>("/connectionHub");

app.Run();