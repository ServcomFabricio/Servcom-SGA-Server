using Microsoft.EntityFrameworkCore;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Models;
using Servcom.SGA.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Servcom.SGA.Infra.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected ServcomSGAContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(ServcomSGAContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Registrar(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }


        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
