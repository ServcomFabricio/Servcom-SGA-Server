using Servcom.SGA.Domain.Configuracao;
using Servcom.SGA.Domain.Configuracao.Repository;
using Servcom.SGA.Infra.Data.Context;
using System.Linq;

namespace Servcom.SGA.Infra.Data.Repository
{
    public class ConfiguracaoGeralRepository : Repository<ConfiguracaoGeral>, IConfiguracaoGeralRepository
    {
        public ConfiguracaoGeralRepository(ServcomSGAContext context) : base(context)
        {
        }

        public ConfiguracaoGeral ObterConfiguracaoGeral()
        {
            return DbSet.ToList().FirstOrDefault();
        }
    }
}
