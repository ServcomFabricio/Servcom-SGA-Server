using Servcom.SGA.Domain.Atendimentos;
using Servcom.SGA.Domain.Atendimentos.Repository;
using Servcom.SGA.Infra.Data.Context;

namespace Servcom.SGA.Infra.Data.Repository
{
    public class AtendimentoRepository : Repository<Atendimento>, IAtendimentoRepository
    {
        public AtendimentoRepository(ServcomSGAContext context) : base(context)
        {
        }
    }
}
