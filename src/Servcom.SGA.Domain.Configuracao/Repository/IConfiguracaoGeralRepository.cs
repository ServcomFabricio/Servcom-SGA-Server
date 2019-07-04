using Servcom.SGA.Domain.Core.Interfaces;

namespace Servcom.SGA.Domain.Configuracao.Repository
{
    public interface IConfiguracaoGeralRepository:IRepository<ConfiguracaoGeral>
    {
        ConfiguracaoGeral ObterConfiguracaoGeral();
    }
}
