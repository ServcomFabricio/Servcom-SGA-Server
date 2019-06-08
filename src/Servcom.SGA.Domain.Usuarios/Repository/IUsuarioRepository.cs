using Servcom.SGA.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Servcom.SGA.Domain.Usuarios.Repository
{
    public interface IUsuarioRepository:IRepository<Usuario>
    {
        IEnumerable<Usuario> ObterUsuarios();
    }
}
