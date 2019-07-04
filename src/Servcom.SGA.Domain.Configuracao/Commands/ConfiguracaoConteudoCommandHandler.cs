using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servcom.SGA.Domain.Configuracao.Events;
using Servcom.SGA.Domain.Configuracao.Repository;
using Servcom.SGA.Domain.Core.Handlers;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;

namespace Servcom.SGA.Domain.Configuracao.Commands
{
    public class ConfiguracaoConteudoCommandHandler : CommandHandler,
        IRequestHandler<IncluirConfiguracaoConteudoCommand,bool>,
        IRequestHandler<EditarConfiguracaoConteudoCommand,bool>,
        IRequestHandler<ExcluirConfiguracaoConteudoCommand,bool>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IConfiguracaoConteudoRepository _configuracaoConteudoRepository;
        private readonly DomainNotificationHandler _notifications;

        public ConfiguracaoConteudoCommandHandler(IUnitOfWork uow,
                                                  IMediatorHandler mediator,
                                                  INotificationHandler<DomainNotification> notifications,
                                                  IConfiguracaoConteudoRepository configuracaoConteudoRepository)
                                                  : base(uow, mediator, notifications)
        {
            _notifications =(DomainNotificationHandler) notifications;
            _mediator = mediator;
            _configuracaoConteudoRepository = configuracaoConteudoRepository;

        }

        public Task<bool> Handle(IncluirConfiguracaoConteudoCommand request, CancellationToken cancellationToken)
        {
            var configuracaoConteudo = new ConfiguracaoConteudo(request.Id, request.Tipo, request.Descricao, request.Ativo, request.Conteudo);

            if (!configuracaoConteudo.EhValido())
            {
                NotificarValidacoesErro(configuracaoConteudo.ValidationResult);
                return Task.FromResult(true);
            }

            _configuracaoConteudoRepository.Registrar(configuracaoConteudo);

            if (Commit())
            {
                _mediator.PublicarEvento(new ConfiguracaoConteudoRegistradoEvent(configuracaoConteudo.Id, configuracaoConteudo.Tipo, configuracaoConteudo.Descricao, configuracaoConteudo.Ativo, configuracaoConteudo.Conteudo));
            }
            return Task.FromResult(true);
        }

        public Task<bool> Handle(EditarConfiguracaoConteudoCommand request, CancellationToken cancellationToken)
        {
            if (!ConfiguracaoConteudoExistente(request.Id, request.MessageType)) return Task.FromResult(true);

            var configuracaoConteudo = new ConfiguracaoConteudo(request.Id, request.Tipo, request.Descricao, request.Ativo, request.Conteudo);
            
            if (!configuracaoConteudo.EhValido())
            {
                NotificarValidacoesErro(configuracaoConteudo.ValidationResult);
                return Task.FromResult(true);
            }

            _configuracaoConteudoRepository.Atualizar(configuracaoConteudo);

            if (Commit())
            {
                _mediator.PublicarEvento(new ConfiguracaoConteudoAtualizadoEvent(configuracaoConteudo.Id, configuracaoConteudo.Tipo, configuracaoConteudo.Descricao, configuracaoConteudo.Ativo, configuracaoConteudo.Conteudo));
            }
            return Task.FromResult(true);
        }

        public Task<bool> Handle(ExcluirConfiguracaoConteudoCommand request, CancellationToken cancellationToken)
        {
            if (!ConfiguracaoConteudoExistente(request.Id, request.MessageType)) return Task.FromResult(true);

            _configuracaoConteudoRepository.Remover(request.Id);

            if (Commit())
            {
                _mediator.PublicarEvento(new ConfiguracaoConteudoExcluidoEvent(request.Id));
            }
            return Task.FromResult(true);

        }

        private bool ConfiguracaoConteudoExistente(Guid id, string messageType)
        {
            var configuracaoConteudo = _configuracaoConteudoRepository.ObterPorId(id);

            if (configuracaoConteudo != null) return true;

            _mediator.PublicarEvento(new DomainNotification(messageType, "Configuracao de conteudo não encontrada."));
            return false;
        }
    }
}
