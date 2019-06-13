using MediatR;
using Servcom.SGA.Domain.Core.Handlers;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using Servcom.SGA.Domain.Usuarios.Events;
using Servcom.SGA.Domain.Usuarios.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Usuarios.Commands
{
    public class UsuarioCommandHandler : CommandHandler,
        IRequestHandler<RegistrarUsuarioCommand,bool>,
        IRequestHandler<ExcluirUsuarioCommand,bool>,
        IRequestHandler<EditarUsuarioCommand, bool>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IUser _user;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioCommandHandler(IUnitOfWork uow,
                                     IMediatorHandler mediator,
                                     IUser user,
                                     INotificationHandler<DomainNotification> notifications,
                                     IUsuarioRepository usuarioRepository) 
                      : base(uow, mediator, notifications)
        {
            _usuarioRepository = usuarioRepository;
            _user = user;
            _mediator = mediator;
        }

        public Task<bool> Handle(ExcluirUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!usuarioExistente(request.Id, request.MessageType)) return Task.FromResult(true);

            var usuario = _usuarioRepository.ObterPorId(request.Id);

            if (usuario.Id == _user.GetUserId())
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "É necessário que outro usuário faça essa ação"));
                return Task.FromResult(true);
            }

            // Validacoes de negocio


            _usuarioRepository.Remover(usuario.Id);

            if (Commit())
            {
                _mediator.PublicarEvento(new UsuarioExcluidoEvent(usuario.Id));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario(request.Id,request.Sigla, request.Nome,request.Setor);

            if (!usuario.EhValido())
            {
                NotificarValidacoesErro(usuario.ValidationResult);
                return Task.FromResult(true);
            }

            var usuarioExistente = _usuarioRepository.Buscar(o => o.Sigla == usuario.Sigla);

            if (usuarioExistente.Any())
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Sigla já utilizada"));
            }

            _usuarioRepository.Registrar(usuario);

            if (Commit())
            {
                _mediator.PublicarEvento(new UsuarioRegistradoEvent(usuario.Id, usuario.Sigla, usuario.Nome,usuario.Setor));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(EditarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario(request.Id, request.Sigla, request.Nome, request.Setor);

            if (!usuario.EhValido())
            {
                NotificarValidacoesErro(usuario.ValidationResult);
                return Task.FromResult(true);
            }

            var usuarioEditado = _usuarioRepository.ObterPorId(usuario.Id);

            if (usuarioEditado.Sigla != usuario.Sigla)
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Sigla Não pode ser alterada"));
            }

            _usuarioRepository.Atualizar(usuario);
            var com = Commit();
            if (com)
            {
                _mediator.PublicarEvento(new UsuarioAtualizadoEvent(usuario.Id, usuario.Sigla, usuario.Nome, usuario.Setor));
            }

            return Task.FromResult(true);
        }

        private bool usuarioExistente(Guid id, string messageType)
        {
            var usuario = _usuarioRepository.ObterPorId(id);

            if (usuario != null) return true;

            _mediator.PublicarEvento(new DomainNotification(messageType, "Usuario não encontrado."));
            return false;
        }


    }
}
