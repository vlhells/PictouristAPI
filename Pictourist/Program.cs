using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Areas.Admin.Services;
using PictouristAPI.Services;

namespace PictouristAPI
{
    public class Program
	{
		public static async Task Main()
		{
			//TODO:

			// Docker.

			// Upload and watch images.
			// Chattin w/ Signal(?)
			// Likes/Comms.

			//Refactor.

			var builder = WebApplication.CreateBuilder();

			string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            //builder.Services.AddDbContext<PictouristContext>(options =>
            //	options.UseSqlServer(connection));

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pictourist API",
                    Description = "API for Pictourist social network.",
                    Contact = new OpenApiContact
                    {
                        Name = "Authors' personal telegram",
                        Url = new Uri("https://t.me/VladislavGashenko")
                    }
                });
            });

            builder.Services.AddDbContext<PictouristContext>(options => options.UseNpgsql(connection));

            builder.Services.AddTransient<IUserValidator<User>, MyUserValidator>();

			builder.Services.AddScoped<IFriendsService, FriendsService>();
			builder.Services.AddScoped<IAccountService, AccountService>();
			builder.Services.AddScoped<IRolesService, RolesService>();
			builder.Services.AddScoped<PictouristAPI.Areas.Admin.Services.IUsersService,
										PictouristAPI.Areas.Admin.Services.UsersService>();
			builder.Services.AddScoped<PictouristAPI.Services.IUsersService,
							PictouristAPI.Services.UsersService>();

			//builder.Services.AddTransient<IPasswordValidator<User>, MyPasswordValidator>();

			builder.Services.AddIdentity<User, IdentityRole>(opts =>
			{
				opts.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<PictouristContext>();

			builder.Services.AddControllers();

            var app = builder.Build();

			app.UseStatusCodePages();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

            using (var scope = app.Services.CreateScope()) // Init db w/ roles and admin if db clean.
            {
                var services = scope.ServiceProvider;

                try
                {
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await InitDbService.InitializeAsync(userManager, rolesManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            app.Run();
		}
	}
}