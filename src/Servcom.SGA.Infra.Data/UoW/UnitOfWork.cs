using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Infra.Data.Context;

namespace Servcom.SGA.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ServcomSGAContext _context;

        public UnitOfWork(ServcomSGAContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            var sav = _context.SaveChanges();
            return sav > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
