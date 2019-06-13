using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Servcom.SGA.Domain.Atendimentos.Commands;
using Servcom.SGA.Domain.Atendimentos.Commands.CommandsTipoAtendimento;
using Servcom.SGA.Domain.Atendimentos.Events;
using Servcom.SGA.Domain.Atendimentos.Events.EventsTipoAtendimento;
using Servcom.SGA.Domain.Atendimentos.Repository;
using Servcom.SGA.Domain.Core.Handlers;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using Servcom.SGA.Domain.Usuarios.Commands;
using Servcom.SGA.Domain.Usuarios.Events;
using Servcom.SGA.Domain.Usuarios.Repository;
using Servcom.SGA.Infra.CrossCutting.Identity;
using Servcom.SGA.Infra.CrossCutting.Identity.Messages;
using Servcom.SGA.Infra.Data.Context;
using Servcom.SGA.Infra.Data.Repository;
using Servcom.SGA.Infra.Data.UoW;

namespace Servcom.SGA.Infra.CrossCutting.IoC.InjectorDependecies
{
    public class InjectorDependecy
    {
        public static void RegisterServices(IServiceCollection services)
        {
  
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegistrarUsuarioCommand,bool>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirUsuarioCommand,bool>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<EditarUsuarioCommand,bool>, UsuarioCommandHandler>();

            services.AddScoped<IRequestHandler<IncluirTipoAtendimentoCommand,bool>, TipoAtendimentoCommandHandler>();
            services.AddScoped<IRequestHandler<EditarTipoAtendimentoCommand,bool>, TipoAtendimentoCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirTipoAtendimentoCommand,bool>, TipoAtendimentoCommandHandler>();

           services.AddScoped<IRequestHandler<IncluirAtendimentoCommand, bool>, AtendimentoCommandHandler>();


            //Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<UsuarioRegistradoEvent>, UsuarioEventHandler>();
            services.AddScoped<INotificationHandler<UsuarioExcluidoEvent>, UsuarioEventHandler>();
            services.AddScoped<INotificationHandler<UsuarioAtualizadoEvent>, UsuarioEventHandler>();

            services.AddScoped<INotificationHandler<TipoAtendimentoRegistradoEvent>, TipoAtendimentoEventHandler>();
            services.AddScoped<INotificationHandler<TipoAtendimentoAtualizadoEvent>, TipoAtendimentoEventHandler>();
            services.AddScoped<INotificationHandler<TipoAtendimentoExcluidoEvent>, TipoAtendimentoEventHandler>();

            services.AddScoped<INotificationHandler<AtendimentoRegistradoEvent>, AtendimentoEventHandler>();


            // Infra - Data
            services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();
            services.AddScoped<ITipoAtendimentoRepository, TipoAtendimentoRepository>();
            services.AddScoped<IUsuarioRepository,UsuarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ServcomSGAContext>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
            services.AddScoped<IdentityErrorDescriber, PortugueseIdentityErrorDescriber>();

            

       
        }
    }
}
