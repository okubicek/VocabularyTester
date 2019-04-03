using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using VocabularyPracticeWeb.Models.Authentication;
using Castle.Windsor;
using Castle.Facilities.AspNetCore;
using VocabularyPracticeWeb.Controllers;
using VocabularyPracticeWeb.Bootstrap.Installers;
using VocabularyPracticeWeb.Domain.Users;

namespace VocabularyPracticeWeb
{
	public class Startup
	{
		private WindsorContainer _container;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			_container = new WindsorContainer();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddIdentityServer(options => {
			//			options.UserInteraction.LoginUrl = "/authentication/login";
			//		})
			//		.AddInMemoryClients(new List<IdentityServer4.Models.Client>())
			//		.AddInMemoryApiResources(new List<IdentityServer4.Models.ApiResource>());

			_container.AddFacility<AspNetCoreFacility>(f => f.CrossWiresInto(services));

			services.AddDbContext<ApplicationDbContext>
					(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, ApplicationRole>()
					.AddEntityFrameworkStores<ApplicationDbContext>()
					.AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.Name = "Cookie";
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
				options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
				options.SlidingExpiration = true;
				options.LoginPath = new PathString("/authentication/login");
			});

			services.AddFluentEmail("defaultEmail@test.cz");

			services.AddAuthorization(options =>
			{
				options.AddPolicy("IsAdministrator", policy =>
				{
					policy.RequireRole(AuthorisationRoles.Admin);
				});
			});

			_container.Install(new WebInstaller());

			services.AddMvc();
			services.AddWindsor(_container, x => x.UseEntryAssembly(typeof(HomeController).Assembly));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			_container.GetFacility<AspNetCoreFacility>().RegistersMiddlewareInto(app);

			app.UseStaticFiles();

			//app.UseIdentityServer();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "areas",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}