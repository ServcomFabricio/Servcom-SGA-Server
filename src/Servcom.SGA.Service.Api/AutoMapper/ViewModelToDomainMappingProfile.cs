using AutoMapper;
using Servcom.SGA.Domain.Atendimentos.Commands;
using Servcom.SGA.Domain.Atendimentos.Commands.CommandsTipoAtendimento;
using Servcom.SGA.Domain.Usuarios.Commands;
using Servcom.SGA.Service.Api.ViewModels;
using Servcom.SGA.Service.Api.ViewModels.Atendimento;

namespace Servcom.SGA.Service.Api.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UsuarioViewModel, RegistrarUsuarioCommand>()
                .ConstructUsing(u => new RegistrarUsuarioCommand(u.Id,u.Sigla, u.Nome,u.Setor));

            CreateMap<TipoAtendimentoViewModel, IncluirTipoAtendimentoCommand>()
                .ConstructUsing(t => new IncluirTipoAtendimentoCommand(t.Tipo, t.Descricao, t.Prioritario));
            CreateMap<TipoAtendimentoViewModel, EditarTipoAtendimentoCommand>()
                .ConstructUsing(t => new EditarTipoAtendimentoCommand(t.Id,t.Tipo, t.Descricao, t.Prioritario));
            CreateMap<TipoAtendimentoViewModel, ExcluirTipoAtendimentoCommand>()
                .ConstructUsing(t => new ExcluirTipoAtendimentoCommand(t.Id));

            CreateMap<AtendimentoViewModel, IncluirAtendimentoCommand>()
             .ConstructUsing(a => new IncluirAtendimentoCommand(a.Id,a.TipoId));


        }
    }
}