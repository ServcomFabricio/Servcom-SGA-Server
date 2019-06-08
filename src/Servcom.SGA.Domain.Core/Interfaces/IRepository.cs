using Servcom.SGA.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Servcom.SGA.Domain.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        void Registrar(TEntity obj);
        TEntity ObterPorId(Guid id);
        void Atualizar(TEntity obj);
        void Remover(Guid id);
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
    }
}
