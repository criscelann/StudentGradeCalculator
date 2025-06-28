using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Add MySQL service (DI)
builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔧 Add MVC support
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🌐 Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ✅ Required to serve CSS, JS, etc.

app.UseRouting();

app.UseAuthorization();

// 📌 Default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
