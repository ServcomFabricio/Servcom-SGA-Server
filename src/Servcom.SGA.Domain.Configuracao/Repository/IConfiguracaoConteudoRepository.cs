using Servcom.SGA.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace Servcom.SGA.Domain.Configuracao.Repository
{
    public interface IConfiguracaoConteudoRepository:IRepository<ConfiguracaoConteudo>
    {
        IEnumerable<ConfiguracaoConteudo> ObterConteudoTodos();
        IEnumerable<ConfiguracaoConteudo> ObterConteudoLista();
    }
}
