using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Servcom.Service.API.Controllers;
using Servcom.SGA.Domain.Atendimentos.Commands;
using Servcom.SGA.Domain.Atendimentos.Commands.CommandsTipoAtendimento;
using Servcom.SGA.Domain.Atendimentos.Repository;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using Servcom.SGA.Service.Api.Configurations;
using Servcom.SGA.Service.Api.ViewModels.Atendimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servcom.SGA.Service.Api.Controllers
{
    public class AtendimentoController : BaseController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly ITipoAtendimentoRepository _tipoAtendimentoRepository;
        private readonly IAtendimentoRepository _atendimentoRepository;
        private IHubContext<SignalRConfiguration> _hubPainel;
        public AtendimentoController(INotificationHandler<DomainNotification> notifications,
                                     IUser user,
                                     IMapper mapper,
                                     ITipoAtendimentoRepository tipoAtendimentoRepository,
                                     IAtendimentoRepository atendimentoRepository,
                                     IMediatorHandler mediator,
                                     IHubContext<SignalRConfiguration> hubPainel) : base(notifications, user, mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _tipoAtendimentoRepository = tipoAtendimentoRepository;
            _atendimentoRepository = atendimentoRepository;
            _hubPainel = hubPainel;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("nova-chamada-atendimento/{idAtendimento:guid}")]
        public IActionResult NovaChamadaAtendimento(Guid idAtendimento)
        {
            var atendimento = _mapper.Map<AtendimentoViewModel>(_atendimentoRepository.ObterPorId(idAtendimento));
            if (atendimento == null) {
                NotificarErro("Erro", "Atendimento não encontrado");
                return Response(); 
            }
            var result= _mapper.Map<IEnumerable<AtendimentoViewModel>>(_atendimentoRepository.painelAtendimento(3));
            var listaAtendimento = new List<AtendimentoViewModel>() { atendimento };
            var response = listaAtendimento.Union(result);
            _hubPainel.Clients.All.SendAsync("painelAtendimento", response);
            return Response("OK");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("incluir-atendimento/{tipoId:guid}")]
        public IActionResult novoAtendimento(Guid tipoId)
        {
            var model =new AtendimentoViewModel() { TipoId=tipoId };
            var incluirCommand = _mapper.Map<IncluirAtendimentoCommand>(model);
             _mediator.EnviarComando(incluirCommand);

            if (!OperacaoValida()) return Response(model);
            
            var response = _atendimentoRepository.ObterPorId(incluirCommand.Id);
            return Response(_mapper.Map<AtendimentoViewModel>(response));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("proximo-atendimento")]
        public async Task<IActionResult> proximoAtendimento([FromBody] ProximoAtendimentoViewModel model)
        {
            var proximoAtendimentoCommand = _mapper.Map<ProximoAtendimentoCommand>(model);
            var result = await _mediator.EnviarComandoEntity(proximoAtendimentoCommand);
            if (!OperacaoValida()) return Response();
            await _hubPainel.Clients.All.SendAsync("painelAtendimento", _mapper.Map<IEnumerable<AtendimentoViewModel>>(_atendimentoRepository.painelAtendimento(4)));
            var response = _mapper.Map<AtendimentoViewModel>(result);
            return Response(response);
        }


        [HttpPost]
        [Authorize(Policy = "PodeGravar")]
        [Route("incluir-tipo-atendimento")]
        public async Task<IActionResult> novoTipoAtendimento([FromBody] TipoAtendimentoViewModel model)
        {
            var incluirCommand = _mapper.Map<IncluirTipoAtendimentoCommand>(model);
            await _mediator.EnviarComando(incluirCommand);
            return Response(incluirCommand);
        }

        [HttpPut]
        [Authorize(Policy = "PodeGravar")]
        [Route("editar-tipo-atendimento")]
        public async Task<IActionResult> atualizarTipoAtendimento([FromBody] TipoAtendimentoViewModel model)
        {
            var editarCommand = _mapper.Map<EditarTipoAtendimentoCommand>(model);
            await _mediator.EnviarComando(editarCommand);
            return Response(editarCommand);
        }

        [HttpDelete]
        [Authorize(Policy = "PodeGravar")]
        [Route("excluir-tipo-atendimento/{id:guid}")]
        public async Task<IActionResult> excluirTipoAtendimento(Guid id)
        {
            var tipoAtendimento = new TipoAtendimentoViewModel() { Id = id };
            var excluirCommand = _mapper.Map<ExcluirTipoAtendimentoCommand>(tipoAtendimento);
            await _mediator.EnviarComando(excluirCommand);
            return Response(excluirCommand);
        }

        [HttpGet]
        [Authorize(Policy = "PodeGravar")]
        [Route("listar-tipos-atendimento")]
        public IEnumerable<TipoAtendimentoViewModel> Get()
        {
            return _mapper.Map<IEnumerable<TipoAtendimentoViewModel>>(_tipoAtendimentoRepository.obterTodos());
        }
        [HttpGet]
        [Authorize(Policy = "PodeGravar")]
        [Route("obter-tipo-atendimento/{id:guid}")]
        public TipoAtendimentoViewModel Get(Guid id)
        {
            return _mapper.Map<TipoAtendimentoViewModel>(_tipoAtendimentoRepository.ObterPorId(id));
        }

     

    }
}
