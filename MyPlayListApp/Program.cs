using Microsoft.EntityFrameworkCore;
using MyPlayListApp.Data.Context;
using MyPlayListApp.Data.Helpers;
using MyPlayListApp.Data.Interfaces;
using MyPlayListApp.Data.Repositories;
using MyPlayListApp.Data.Services;

namespace MyPlayListApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MyPlayListAppContext>(options =>
                                                                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
            builder.Services.AddScoped<ISongRepository, SongRepository>();
            builder.Services.AddScoped<ISingerRepository, SingerRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISongService, SongService>();
            builder.Services.AddScoped<ISingerService, SingerService>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MyPlayListAppContext>();

                var pendingMigrations = dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    dbContext.Database.Migrate();
                }
            }

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
        }
    }
}
