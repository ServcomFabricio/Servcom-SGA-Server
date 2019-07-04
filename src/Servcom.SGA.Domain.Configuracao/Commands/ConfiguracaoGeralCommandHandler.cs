using MediatR;
using Servcom.SGA.Domain.Configuracao.Events;
using Servcom.SGA.Domain.Configuracao.Repository;
using Servcom.SGA.Domain.Core.Handlers;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Configuracao.Commands
{
    public class ConfiguracaoGeralCommandHandler : CommandHandler,
        IRequestHandler<EditarConfiguracaoGeralCommand,bool>


    {
        private readonly IConfiguracaoGeralRepository _configuracaoGeralRepository;
        private readonly IMediatorHandler _mediator;


        public ConfiguracaoGeralCommandHandler(IUnitOfWork uow,
                                               IMediatorHandler mediator,
                                               INotificationHandler<DomainNotification> notifications,
                                               IConfiguracaoGeralRepository configuracaoGeralRepository)
                                               : base(uow, mediator, notifications)
        {
            _configuracaoGeralRepository = configuracaoGeralRepository;
            _mediator = mediator;
        }

        public Task<bool> Handle(EditarConfiguracaoGeralCommand request, CancellationToken cancellationToken)
        {
            var configuracaoGeral = new ConfiguracaoGeral(request.Id, request.TituloPainelAtendimento, request.TextoFixoPainelAtendimento,request.EntradaVideo,request.ConteudoConfigurado);

            if (!configuracaoGeral.EhValido())
            {
                NotificarValidacoesErro(configuracaoGeral.ValidationResult);
                return null;
            }

            var configuracaoGeralEditado = _configuracaoGeralRepository.Buscar(c=>c.Id==request.Id);

            if (configuracaoGeralEditado == null)
            {
                _configuracaoGeralRepository.Registrar(configuracaoGeral);
            } else {
                _configuracaoGeralRepository.Atualizar(configuracaoGeral);
            }

            if (Commit())
            {
                _mediator.PublicarEvento(new ConfiguracaoGeralAtualizadoEvent(configuracaoGeral.Id, configuracaoGeral.TituloPainelAtendimento, configuracaoGeral.TextoFixoPainelAtendimento, configuracaoGeral.EntradaVideo, configuracaoGeral.ConteudoConfigurado));
            }
            return Task.FromResult(true);

        }
    }
}
