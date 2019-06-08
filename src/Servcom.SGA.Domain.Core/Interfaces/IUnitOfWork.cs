using System;

namespace Servcom.SGA.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
