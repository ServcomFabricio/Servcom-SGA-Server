using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servcom.Service.API.Controllers;
using Servcom.SGA.Domain.Configuracao.Commands;
using Servcom.SGA.Domain.Configuracao.Repository;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using Servcom.SGA.Service.Api.ViewModels.Configuracao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servcom.SGA.Service.Api.Controllers
{
    public class ConfiguracaoController : BaseController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly IConfiguracaoGeralRepository _configuracaoGeralRepository;
        private readonly IConfiguracaoConteudoRepository _configuracaoConteudoRepository;

        public ConfiguracaoController(INotificationHandler<DomainNotification> notifications,
                                      IUser user,
                                      IMediatorHandler mediator,
                                      IMapper mapper,
                                      IConfiguracaoGeralRepository configuracaoGeralRepository,
                                      IConfiguracaoConteudoRepository configuracaoConteudoRepository)
                                      : base(notifications, user, mediator)
        {
            _configuracaoGeralRepository = configuracaoGeralRepository;
            _configuracaoConteudoRepository = configuracaoConteudoRepository;
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        [Authorize(Policy = "PodeGravar")]
        [Route("obter-configuracao-geral")]
        public ConfiguracaoGeralViewModel ObterConfiguracaoGeral()
        {
            return _mapper.Map<ConfiguracaoGeralViewModel>(_configuracaoGeralRepository.ObterConfiguracaoGeral());
        }

        [HttpPost]
        [Authorize(Policy = "PodeGravar")]
        [Route("atualizar-configuracao-geral")]
        public async Task<IActionResult> AtualizarConfiguracaoGeral([FromBody] ConfiguracaoGeralViewModel model)
        {
            var editarCommand = _mapper.Map<EditarConfiguracaoGeralCommand>(model);
            await _mediator.EnviarComando(editarCommand);
            return Response(editarCommand);
        }

        [HttpGet]
        [Authorize(Policy = "PodeGravar")]
        [Route("obter-configuracao-conteudo-lista")]
        public IEnumerable<ConfiguracaoConteudoViewModel> ObterConfiguracaoConteudoLista()
        {
            return _mapper.Map<IEnumerable<ConfiguracaoConteudoViewModel>>(_configuracaoConteudoRepository.ObterConteudoLista()); 
        }

        [HttpGet]
        [Authorize(Policy = "PodeGravar")]
        [Route("obter-configuracao-conteudo-todos")]
        public IEnumerable<ConfiguracaoConteudoViewModel> ObterConfiguracaoConteudoTodos()
        {
            return _mapper.Map<IEnumerable<ConfiguracaoConteudoViewModel>>(_configuracaoConteudoRepository.ObterConteudoTodos());
        }

        [HttpGet]
        [Authorize(Policy = "PodeGravar")]
        [Route("obter-configuracao-conteudo/{id:guid}")]
        public ConfiguracaoConteudoViewModel ObterConfiguracaoConteudo(Guid id)
        {
            return _mapper.Map<ConfiguracaoConteudoViewModel>(_configuracaoConteudoRepository.ObterPorId(id));
        }

        [HttpPost]
        [Authorize(Policy = "PodeGravar")]
        [Route("incluir-configuracao-conteudo")]
        public async Task<IActionResult> IngluirConfiguracaoConteudo([FromBody] ConfiguracaoConteudoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var editarCommand = _mapper.Map<IncluirConfiguracaoConteudoCommand>(model);
            await _mediator.EnviarComando(editarCommand);
            return Response(editarCommand);
        }

        [HttpPost]
        [Authorize(Policy = "PodeGravar")]
        [Route("editar-configuracao-conteudo")]
        public async Task<IActionResult> EditarConfiguracaoConteudo([FromBody] ConfiguracaoConteudoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var editarCommand = _mapper.Map<EditarConfiguracaoConteudoCommand>(model);
            await _mediator.EnviarComando(editarCommand);
            return Response(editarCommand);
        }

        [HttpDelete]
        [Authorize(Policy = "PodeGravar")]
        [Route("excluir-configuracao-conteudo/{id:guid}")]
        public async Task<IActionResult> ExcluirConfiguracaoConteudo(Guid id)
        {
            var editarCommand = new ExcluirConfiguracaoConteudoCommand(id);
            await _mediator.EnviarComando(editarCommand);
            return Response(editarCommand);
        }
    }
}
