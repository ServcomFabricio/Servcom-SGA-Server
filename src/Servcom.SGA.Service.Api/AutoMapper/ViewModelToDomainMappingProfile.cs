using AutoMapper;
using Servcom.SGA.Domain.Usuarios.Commands;
using Servcom.SGA.Service.Api.ViewModels;

namespace Servcom.SGA.Service.Api.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            // Usuario
            CreateMap<UsuarioViewModel, RegistrarUsuarioCommand>()
                .ConstructUsing(c => new RegistrarUsuarioCommand(c.Id,c.Sigla, c.Nome,c.Setor));
        }
    }
}