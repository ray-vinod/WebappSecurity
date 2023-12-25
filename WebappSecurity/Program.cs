var builder = WebApplication.CreateBuilder(args);
{

    // Add services to the container.
    builder.Services.AddRazorPages();

    builder.Services.AddAuthentication("appCookie").AddCookie("appCookie", options =>
    {
        options.Cookie.Name = "appCookie";
        options.ExpireTimeSpan = TimeSpan.FromSeconds(120);
        options.SlidingExpiration = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

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
