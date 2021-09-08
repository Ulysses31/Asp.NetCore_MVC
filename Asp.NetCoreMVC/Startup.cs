using Asp.NetCoreMVC.Models;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;

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

			// Localization
			services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
			services.AddMvc()
				.AddViewLocalization(
						LanguageViewLocationExpanderFormat.Suffix,
						opts => { opts.ResourcesPath = "Resources"; })
				.AddDataAnnotationsLocalization();
			services.Configure<RequestLocalizationOptions>(
				options => {
					var supportedCultures = new List<CultureInfo>
					{
								// new CultureInfo("fr"),
								// new CultureInfo("fr-FR"),
								// new CultureInfo("nl"),
								// new CultureInfo("nl-BE"),
								new CultureInfo("en-US"),
								new CultureInfo("el-GR")
								// new CultureInfo("el"),
						};

					options.DefaultRequestCulture = new RequestCulture("en-US");
					options.SupportedCultures = supportedCultures;
					options.SupportedUICultures = supportedCultures;
				});

			// services.AddScoped<IPieRepository, MockPieRepository>();
			// services.AddScoped<ICategoryRepository, MockCategoryRepository>();
			services.AddScoped<IPieRepository, PieRepository>();
			services.AddScoped<IPieReviewRepository, PieReviewRepository>();
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
			app.UseRequestLocalization(
				app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value
			);


			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
									name: "default",
									pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}