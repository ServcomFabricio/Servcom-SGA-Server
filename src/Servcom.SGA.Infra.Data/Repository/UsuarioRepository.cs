using Microsoft.EntityFrameworkCore;
using Servcom.SGA.Domain.Usuarios;
using Servcom.SGA.Domain.Usuarios.Repository;
using Servcom.SGA.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servcom.SGA.Infra.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ServcomSGAContext context) : base(context)
        {
       
        }

        public IEnumerable<Usuario> ObterUsuarios()
        {
            return DbSet.ToList();
        }
    }
}
