using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servcom.SGA.Infra.CrossCutting.AspNetFilters;
using Servcom.SGA.Infra.CrossCutting.Identity;
using Servcom.SGA.Service.Api.Configurations;
using System.Collections.Generic;
using System.Linq;

namespace Servcom.SGA.Service.Api
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
            // Contexto do EF para o Identity
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Servcom")));

            // Configurações de Autenticação, Autorização e JWT.
            services.AddMvcSecurity(Configuration);

            //configuração Idenity
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;
       
            });

  
            // incluir SignalR
            services.AddSignalR();

            // busca ipadrress do arquivo appsettings            
            var  AuthAdrressList = Configuration.GetSection("AuthorizeCors:ips").GetChildren().ToList();
            var authAdrress=new List<string>();
            foreach (var item in AuthAdrressList)
            {
                authAdrress.Add(item.Value);
            }

            // inlcui CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(authAdrress.ToArray())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Options para configurações customizadas
            services.AddOptions();

            services.AddMvc(options =>
            {
                //TODO:logger
                //options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
                options.Filters.Add(typeof(ValidateModelAttribute));
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());


            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // AutoMapper
            services.AddAutoMapper();

            // Configurações do Swagger
            services.AddSwaggerConfig();

            // MediatR
            services.AddMediatR(typeof(Startup));

            // Registrar todos os DI
            services.AddDIConfiguration();

        }

        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IHttpContextAccessor accessor)
        {
        
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //somente para IIS
                app.UseHsts();
                //TODO: retirar
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseSignalR(route =>
            {
                route.MapHub<SignalRConfiguration>("/painel-atendimento-client");
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            #region Configurações MVC
            //somente para IIS
            app.UseHttpsRedirection();
            // Arquivo estaticos
            app.UseStaticFiles();
            // Autenticação de usuário
            app.UseAuthentication();
            //MVC
            app.UseMvc();

            #endregion

            #region Swagger
            // Geração de Documentação Swagger       
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Servcom Serviços de Computação - Versão 1.0");
            });
            #endregion
        }
    }

    

   
}
