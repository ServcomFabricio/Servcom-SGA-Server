using System.Collections.Generic;
using System.Linq;
using Servcom.SGA.Domain.Configuracao;
using Servcom.SGA.Domain.Configuracao.Repository;
using Servcom.SGA.Infra.Data.Context;

namespace Servcom.SGA.Infra.Data.Repository
{
    public class ConfiguracaoConteudoRepository : Repository<ConfiguracaoConteudo>, IConfiguracaoConteudoRepository
    {
        public ConfiguracaoConteudoRepository(ServcomSGAContext context) : base(context)
        {
        }

        public IEnumerable<ConfiguracaoConteudo> ObterConteudoLista()
        {
            
            return DbSet.Select(cc => ConfiguracaoConteudo.ConfiguracaoConteudoFactory.ConfiguracaoConteudoLista(cc.Id,cc.Tipo,cc.Descricao,cc.Ativo)).ToList();
        }

        public IEnumerable<ConfiguracaoConteudo> ObterConteudoTodos()
        {

            return DbSet.Where(cc=>cc.Ativo==true).ToList();
        }

    }
}
