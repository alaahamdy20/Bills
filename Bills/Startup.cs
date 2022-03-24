using Bills.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using System;
using Bills.Repository;
using Bills.Services.Interfaces;
using Bills.Services;
using Bills.Models.ModelView;

namespace Bills
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = " ";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession(s => s.IdleTimeout = TimeSpan.FromDays(2));
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("url")).UseLazyLoadingProxies());
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddSwaggerDocument();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                builder =>
                                {
                                    builder.AllowAnyOrigin();
                                    builder.AllowAnyMethod();
                                    builder.AllowAnyHeader();
                                });
            });

            #region reposatory Layout
            services.AddScoped<IBillItemRepository, BillItemRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyTypeRepository, CompanyTypeRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IUnitRepositroy, UnitRepositroy>();
            #endregion


            #region Services Layout
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ITypeDataService, TypeDataService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IReportService, ReportService>(); 
                services.AddScoped<IBillItemService, BillItemService>();
            #endregion
            // services.AddScoped(typeof(ApiModel<>));
            services.AddScoped<ApiModel>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
