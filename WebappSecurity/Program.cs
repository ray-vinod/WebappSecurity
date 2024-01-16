using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebappSecurity.Constants;
using WebappSecurity.Data;
using WebappSecurity.Models.Identity;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    string dbConnection = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? string.Empty;

    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(dbConnection));

    builder.Services.AddIdentity<AppUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

    builder.Services.Configure<AntiforgeryOptions>(options =>
    {
        options.Cookie.Name = Config.CookieForgeryName;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = Config.SchemeName;
    })
    .AddCookie(Config.SchemeName, options =>
    {
        options.Cookie.Name = Config.CookieName;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromSeconds(120);
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

    builder.Services.AddRazorPages();

}

var app = builder.Build();
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
