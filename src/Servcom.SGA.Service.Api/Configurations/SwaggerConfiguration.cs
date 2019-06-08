using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servcom.SGA.Service.Api.Configurations
{
    public static class SwaggerConfiguration
    {


        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1.0", new Info
                {
                    Version = "v1.0",
                    Title = "Servcom - SGA",
                    Description = "Servcom serviços de Computação ",
                    TermsOfService = "Sistema de Gerenciamento de Atendimento",
                    Contact = new Contact { Name = "Equipe Servcom de Desenvolvimento", Email = "contato@servcom.com.br", Url = "http://servcom.com.br/apis/sga" },
                    License = new License { Name = "MIT", Url = "http://servcom.com.br/licensa" }
                });

                s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();

            });

        }
    }

}
