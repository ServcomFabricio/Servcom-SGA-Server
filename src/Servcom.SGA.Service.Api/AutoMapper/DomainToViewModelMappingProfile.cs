using AutoMapper;
using Servcom.SGA.Domain.Atendimentos;
using Servcom.SGA.Domain.Configuracao;
using Servcom.SGA.Domain.Usuarios;
using Servcom.SGA.Service.Api.ViewModels;
using Servcom.SGA.Service.Api.ViewModels.Atendimento;
using Servcom.SGA.Service.Api.ViewModels.Configuracao;

namespace Servcom.SGA.Service.Api.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Usuario, UsuarioViewModel>();

            CreateMap<TipoAtendimento, TipoAtendimentoViewModel>();
            CreateMap<Atendimento, AtendimentoViewModel>();
            CreateMap<ConfiguracaoGeral, ConfiguracaoGeralViewModel>();
            CreateMap<ConfiguracaoConteudo, ConfiguracaoConteudoViewModel>();
        }
    }
}