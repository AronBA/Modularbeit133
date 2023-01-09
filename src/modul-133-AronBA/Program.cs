using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using modul_133_AronBA.Data;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Login/Denied";
      
    });



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbGainzbourgContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbGainzbourgConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
