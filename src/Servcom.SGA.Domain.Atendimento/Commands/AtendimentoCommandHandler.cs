using MediatR;
using Servcom.SGA.Domain.Atendimentos.Events;
using Servcom.SGA.Domain.Atendimentos.Repository;
using Servcom.SGA.Domain.Core.Handlers;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Servcom.SGA.Domain.Atendimentos.Commands
{
    public class AtendimentoCommandHandler : CommandHandler,
        IRequestHandler<IncluirAtendimentoCommand,bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _mediator;
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly DomainNotificationHandler _notifications;
        public AtendimentoCommandHandler(IUnitOfWork uow,
                                         IMediatorHandler mediator,
                                         IAtendimentoRepository atendimentoRepository,
                                         INotificationHandler<DomainNotification> notifications) : base(uow, mediator, notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
            _atendimentoRepository = atendimentoRepository;
        }

        public Task<bool> Handle(IncluirAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var atendimento = new Atendimento(request.Id, request.TipoId);

            if (!atendimento.EhValido())
            {
                NotificarValidacoesErro(atendimento.ValidationResult);
                return Task.FromResult(true);
            }


           var seq =(int) _atendimentoRepository.obterUltimoAtendimento(atendimento.TipoId, atendimento.DataCriacao);
           atendimento.setSequencia(seq + 1);

           _atendimentoRepository.Registrar(atendimento);

           if (_atendimentoRepository.SaveChangesIncluir(atendimento)>0)
           {
             _mediator.PublicarEvento(new AtendimentoRegistradoEvent(atendimento.Id, atendimento.Sequencia,atendimento.DataCriacao, atendimento.TipoId));
           }
           return Task.FromResult(true);
        }
    }
}
