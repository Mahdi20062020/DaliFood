using DaliFood.Models.Data;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace DaliFood.AdminPanel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(
             options =>
             {
                 options.LoginPath = "/Identity/Account/login";
                 options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                 options.LogoutPath = "/Identity/Account/logoutmodel";


             });


            services.AddScoped<UnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddControllersWithViews();
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/").AllowAnonymousToPage("/Index");
                options.Conventions.AllowAnonymousToPage("/Customer/Account/Register");
                options.Conventions.AuthorizeFolder("/CustomersProduct", SD.CustomerPolicy);
                options.Conventions.AuthorizeFolder("/Product", SD.ProductEditorPolicy);
                options.Conventions.AuthorizeFolder("/ProductCategorie", SD.ProductEditorPolicy);
                options.Conventions.AuthorizePage("/Order/Index", SD.AdminPolicy);
                options.Conventions.AuthorizeFolder("/Financial", SD.AdminPolicy);
                options.Conventions.AuthorizeFolder("/Letter", SD.AdminPolicy);
                options.Conventions.AuthorizeFolder("/CustomerType", SD.AdminPolicy);
                options.Conventions.AuthorizeFolder("/Customer/Index", SD.AdminPolicy);
                options.Conventions.AuthorizeFolder("/Order/Item", SD.CustomerPolicy);    
                options.Conventions.AuthorizeFolder("/Customer/Account/Manage", SD.CustomerOwnerPolicy);
                options.Conventions.AuthorizeAreaPage("Identity", "/Account/register", SD.AdminRole);
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(SD.CustomerPolicy, policy => { policy.RequireClaim(SD.CustomerId); policy.RequireRole(SD.CustomerOwnerRole, SD.AdminRole); });
                options.AddPolicy(SD.ProductEditorPolicy, policy => { policy.RequireRole(SD.ProductEditorRole, SD.AdminRole); });
                options.AddPolicy(SD.AdminPolicy, policy => { policy.RequireRole(SD.AdminRole); });
                options.AddPolicy(SD.CustomerOwnerPolicy, policy => { policy.RequireRole(SD.CustomerOwnerRole); });

            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error/Er400");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Error/Er{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
