using ChuborekekCRUD.Data;
using ChuborekekCRUD.Interfaces;
using ChuborekekCRUD.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//setting the database context
builder.Services.AddDbContext<ChuborekekCRUDDbContext>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("ChuborekekCRUDConnection")));
//add the repository to services so we can inject it to controllers
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(ICatRepository), typeof(CatRepository));
builder.Services.AddScoped(typeof(IDogRepository), typeof(DogRepository));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
