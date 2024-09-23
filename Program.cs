using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configure cookie-based authentication.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    // Specify the path to the login page when authentication is required.
    options.LoginPath = new PathString("/");

    // Specify the path to handle cases when a user is denied access due to insufficient permissions.
    options.AccessDeniedPath = new PathString("/");

    // Specify the path to redirect after logout.
    options.LogoutPath = new PathString("/");
});

// Register services required by controllers and views.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware to serve static files like CSS, JS, and images from the 'wwwroot' folder.
app.UseStaticFiles();

// Enable authentication middleware to protect routes that require user login.
app.UseAuthentication();

// Enable authorization middleware to enforce access control based on user roles or claims.
app.UseAuthorization();

// Define the default route pattern for MVC.
// This sets the "Home" controller's "Index" action as the default page (when navigating to "/").
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
