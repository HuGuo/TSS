using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TSS.Models;
using System;

namespace TSS.BLL
{
    public abstract class Repository<TEntity,Tkey> : IDisposable
        where TEntity : class
    {
        protected Context Context { get; private set; }

        protected Repository()
        {
            Context = new Context();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public TEntity Get(Tkey id) 
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IList<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);

            Context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);

            Context.Entry<TEntity>(entity).State = EntityState.Modified;

            Context.SaveChanges();
        }

        public virtual void Update(Tkey keyValue,TEntity entity) {
            TEntity oldEntity = Context.Set<TEntity>().Find(keyValue);
            if (null !=oldEntity) {
                Context.Entry<TEntity>(oldEntity).CurrentValues.SetValues(entity);
            }
            Context.SaveChanges();
        }

        public virtual void Delete(Tkey id)
        {
            if (IsExists(id)) {
                Delete(Get(id));
            }
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);

            Context.Set<TEntity>().Remove(entity);

            Context.SaveChanges();
        }

        public bool IsExists(Tkey id) 
        {
            return null != Get(id);
        }

        public virtual IList<TEntity> PageOf(int pageIndex , int pageSize , out int rowCount) {
            int skipCount = pageSize * (pageIndex - 1);
            rowCount = Context.Set<TEntity>().Count();
            var query = Context.Set<TEntity>().AsNoTracking().Skip(skipCount).Take(pageSize);
            return query.ToList();

        }
    }
}
