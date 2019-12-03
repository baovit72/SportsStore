using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SportsStore
{
    //Services are registered in the ConfigureServices method of the Startup class
    //Prepare neccessary data and service
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. 
        //Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //sets up the services provided by Entity Framework Core for the database context
            services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(Configuration["Data:SportsStoreProducts:ConnectionString"]));
            // //sets up the services provided by Entity Framework Core for the database identity context
            services.AddDbContext<AppIdentityDbContext>(options =>options.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionString"]));
            //set up the Identity services using the built-in classes to represent users and roles
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
            //specifies that a new " EFProductRepository" will be created each time "IProductRepository" interface is needed
            // Dependency Injection
            services.AddTransient<IProductRepository, EFProductRepository>();
            //specifies that the same object should be used for Cart instances request
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddScoped<ImportItems>(sp => SessionImportItems.GetImportItems(sp));
            //use the HttpContextAccessor class when implementations of the IHttpContextAccessor interface are required
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Registering the Order Repository Service
            services.AddTransient<IOrderRepository, EFOrderRepository>(); 
            services.AddTransient<IImportOrderRepository, EFImportOrderRepository>();
            services.AddTransient<IEmployeeRepository, EFEmployeeRepository>();
            services.AddTransient<IIncomeReportRepository, EFIncomeReportRepository>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Enabling Sessions: storing Session State in memory, lost when restart or shutdown app
            services.AddMemoryCache();
            //Needed for storing Session between Redirection
            services.AddDistributedMemoryCache();
            //services.AddSession();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //app.UseHsts();
            }
            //app.UseStatusCodePages();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            //Enabling Sessions
            app.UseSession();
            // set up the components that will intercept requests and responses to implement the security policy
            app.UseAuthentication();
            //create a Product controller to handle the default request
            //Product controller constructor require IProductRepository interface
            app.UseMvc(routes =>
            {
                //Soccer/Page2
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" }
                 );
                //Page2
                routes.MapRoute(
                     name: null,
                     template: "Page{productPage:int}",
                     defaults: new
                 {
                     controller = "Product",
                     action = "List",
                     productPage = 1
                 }
                 );
                //Soccer
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new
                {
                    controller = "Product",
                    action = "List",
                    productPage = 1
                }
                );
                //first page of products from all categories
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new
                {
                    controller = "Product",
                    action = "List",
                    productPage = 1
                });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
                //Custom Pagination page url
                //routes.MapRoute(
                //    name: "pagination",
                //    template: "Products/Page{productPage}",
                //    defaults: new { Controller = "Product", action = "List" });
                //Default page url
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Product}/{action=List}/{id?}");
            });

            //Seed local data for the Product
            //SeedData.EnsurePopulated(app);
            //Seed local data for the Identity
            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
