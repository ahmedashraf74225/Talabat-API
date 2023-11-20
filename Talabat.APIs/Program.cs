
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Controllers;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<StoreContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            // once he open the application he send requests ->
            // so he ask the ram instead of the database.
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                //depencey injection for ConnectionMultiplexer
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityService(builder.Configuration);

            #endregion

            #region kestral
            var app = builder.Build(); //kestral

            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var logerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var dbcontext = service.GetRequiredService<StoreContext>();
                await dbcontext.Database.MigrateAsync();

                await StoreContextSeed.SeedAsync(dbcontext);

                var identityContext = service.GetRequiredService<AppIdentityDbContext>(); // ask object from CLR explicitly
                await identityContext.Database.MigrateAsync();

                var userManager = service.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
                
            }
            catch( Exception ex)
            {
                var logger = logerFactory.CreateLogger<Program>();
                logger.LogError(ex,"An Error occured during applying the Migration");
            }
            #endregion

            #region Configure App [Kestral] Middlewares

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.useSwaggerMiddlewares();
            }

            // to handle NotFound Endpoint
            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}