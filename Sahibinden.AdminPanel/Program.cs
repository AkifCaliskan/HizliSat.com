using Sahibinden.AdminPanel.Middleware;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Concrete.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<AdminAuthMiddleware>();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
