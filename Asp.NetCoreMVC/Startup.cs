using Asp.NetCoreMVC.Models;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Asp.NetCoreMVC
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection"))
			);

			// User Identity
			services.AddDefaultIdentity<IdentityUser>()
				.AddEntityFrameworkStores<AppDbContext>();

			// Toast Notifications
			services.AddNotyf(config => { 
				config.DurationInSeconds = 3; 
				config.IsDismissable = true;
				config.Position = NotyfPosition.BottomRight; 
			});

			// services.AddScoped<IPieRepository, MockPieRepository>();
			// services.AddScoped<ICategoryRepository, MockCategoryRepository>();
			services.AddScoped<IPieRepository, PieRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();

			// get current shopping cart
			services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));

			services.AddControllersWithViews();
			services.AddHttpContextAccessor();
			services.AddSession();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSession();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseNotyf();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
									name: "default",
									pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}