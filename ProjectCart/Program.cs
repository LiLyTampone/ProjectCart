
using Microsoft.EntityFrameworkCore;
using ProjectCart.Data;


var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ Entity Framework Core với SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Thêm Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session tồn tại trong 30 phút
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Bật Session
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

