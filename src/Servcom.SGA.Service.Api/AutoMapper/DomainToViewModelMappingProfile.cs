using AutoMapper;
using Servcom.SGA.Domain.Usuarios;
using Servcom.SGA.Service.Api.ViewModels;

namespace Servcom.SGA.Service.Api.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //CreateMap<Atendimento, AtendimentoViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}