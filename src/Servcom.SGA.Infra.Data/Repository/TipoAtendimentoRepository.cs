using System.Collections.Generic;
using System.Linq;
using Servcom.SGA.Domain.Atendimentos;
using Servcom.SGA.Domain.Atendimentos.Repository;
using Servcom.SGA.Infra.Data.Context;

namespace Servcom.SGA.Infra.Data.Repository
{
    public class TipoAtendimentoRepository : Repository<TipoAtendimento>, ITipoAtendimentoRepository
    {
        public TipoAtendimentoRepository(ServcomSGAContext context) : base(context)
        {
        }

        public IEnumerable<TipoAtendimento> obterTodos()
        {
            return DbSet.ToList().Where(t=>t.Ativo==true);
        }
    }
}
