using AutoMapper;
using Servcom.SGA.Domain.Atendimentos.Commands;
using Servcom.SGA.Domain.Atendimentos.Commands.CommandsTipoAtendimento;
using Servcom.SGA.Domain.Configuracao.Commands;
using Servcom.SGA.Domain.Usuarios.Commands;
using Servcom.SGA.Service.Api.ViewModels;
using Servcom.SGA.Service.Api.ViewModels.Atendimento;
using Servcom.SGA.Service.Api.ViewModels.Configuracao;

namespace Servcom.SGA.Service.Api.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UsuarioViewModel, RegistrarUsuarioCommand>()
                .ConstructUsing(u => new RegistrarUsuarioCommand(u.Id, u.Sigla, u.Nome, u.Setor));

            CreateMap<TipoAtendimentoViewModel, IncluirTipoAtendimentoCommand>()
                .ConstructUsing(t => new IncluirTipoAtendimentoCommand(t.Tipo, t.Descricao, t.Prioritario));
            CreateMap<TipoAtendimentoViewModel, EditarTipoAtendimentoCommand>()
                .ConstructUsing(t => new EditarTipoAtendimentoCommand(t.Id, t.Tipo, t.Descricao, t.Prioritario));
            CreateMap<TipoAtendimentoViewModel, ExcluirTipoAtendimentoCommand>()
                .ConstructUsing(t => new ExcluirTipoAtendimentoCommand(t.Id));

            CreateMap<AtendimentoViewModel, IncluirAtendimentoCommand>()
             .ConstructUsing(a => new IncluirAtendimentoCommand(a.Id, a.Prioritario, a.TipoId));

            CreateMap<ProximoAtendimentoViewModel, ProximoAtendimentoCommand>()
            .ConstructUsing(a => new ProximoAtendimentoCommand(a.Tipo, a.DataCriacao, a.Usuario, a.Prioritario));

            CreateMap<AtendimentoViewModel, EditarAtendimentoCommand>()
                       .ConstructUsing(a => new EditarAtendimentoCommand(a.Id, a.DataHoraInicio, a.DataHoraFim, a.DataHoraultimoReingresso, a.Guiche, a.Status, a.UsuarioId));

            CreateMap<ConfiguracaoGeralViewModel, EditarConfiguracaoGeralCommand>()
                       .ConstructUsing(c => new EditarConfiguracaoGeralCommand(c.Id,c.TituloPainelAtendimento, c.TextoFixoPainelAtendimento,c.EntradaVideo,c.ConteudoConfigurado));

            CreateMap<ConfiguracaoConteudoViewModel, IncluirConfiguracaoConteudoCommand>()
            .ConstructUsing(c => new IncluirConfiguracaoConteudoCommand(c.Id, c.Tipo,c.Descricao,c.Ativo,c.Conteudo));

            CreateMap<ConfiguracaoConteudoViewModel, EditarConfiguracaoConteudoCommand>()
         .ConstructUsing(c => new EditarConfiguracaoConteudoCommand(c.Id, c.Tipo, c.Descricao, c.Ativo, c.Conteudo));
        }
    }
}