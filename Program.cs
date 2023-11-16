using DataExplorerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DataExplorerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Use the configuration from builder to get the connection string
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<VastDataContext>(options => options.UseNpgsql(connectionString));
            // Add services required for using controllers (API)
            builder.Services.AddControllers(); 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            
            // Allow CORS from anywhere
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Add endpoints for controllers (API)
            app.MapControllers();

            app.Run();
        }

    }
}