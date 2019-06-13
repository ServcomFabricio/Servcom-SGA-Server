using Servcom.SGA.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace Servcom.SGA.Domain.Atendimentos.Repository
{
    public interface ITipoAtendimentoRepository:IRepository<TipoAtendimento>
    {
        IEnumerable<TipoAtendimento> obterTodos();

    }
}
