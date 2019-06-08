using Servcom.SGA.Domain.Core.Commands;
using Servcom.SGA.Domain.Core.Events;
using System.Threading.Tasks;

namespace Servcom.SGA.Domain.Core.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task EnviarComando<T>(T comando) where T : Command;
    }
}
