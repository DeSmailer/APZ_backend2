using DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using DataAccessLayer.Models.Interfaces;
using DataAccessLayer.Models.Repositories;
using PresentationLayer.Controllers;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Interfaces;
using PresentationLayer.Hubs;

namespace APZ_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.Configuration = configuration;
            this.Env = env;
        }

        public IWebHostEnvironment Env { get; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            InstallBussinessLogic(services);
            this.InstallDataAccess(services);
            InstallServices(services);
            services.AddSignalR();
            services.BuildServiceProvider().GetService<AppDbContext>().Database.Migrate();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:3000")
                        );
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/hubs/chat");
            });
        }

        private void InstallBussinessLogic(IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
        }
        private void InstallDataAccess(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.BuildServiceProvider().GetService<AppDbContext>().Database.Migrate();
        }
        private void InstallServices(IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
            services.AddScoped<IInstitutionEmployeeService, InstitutionEmployeeService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPaymentHistoryService, PaymentHistoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IExportImportService, ExportImportService>();
            services.AddScoped<IExportImportService, ExportImportService>(); 
            services.AddScoped<AzureFunction.IPayDailyCost, AzureFunction.PayDailyCost>(); 
        }
    }
}