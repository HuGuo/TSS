using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public abstract class Repository<TEntity> : IDisposable
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

        public virtual TEntity Get(object id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IList<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public virtual bool Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);

            return Context.SaveChanges() >0;
        }

        public virtual bool Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry<TEntity>(entity).State = EntityState.Modified;

            return Context.SaveChanges() >0;
        }

        public virtual void Update(object key, TEntity entity)
        {
            TEntity oldEntity = Context.Set<TEntity>().Find(key);
            if (null != oldEntity) {
                Context.Entry<TEntity>(oldEntity).CurrentValues.SetValues(entity);
            }
            Context.SaveChanges();
        }

        public virtual bool Delete(object id)
        {
            return Delete(Get(id));
        }

        public bool Delete(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Set<TEntity>().Remove(entity);

            return Context.SaveChanges() >0;
        }

        public bool IsExists(object id)
        {
            return null != Get(id);
        }

        protected int GetSkip(int pageIndex,int pageSize,int rowCount) {
            if (rowCount < 1) {
                return 0;
            } else {
                int skip = (pageIndex - 1) * pageSize;
                if (skip < 0) {
                    skip = 0;
                }
                if (skip > rowCount) {
                    skip = rowCount - (rowCount % pageSize);
                } else if (skip == rowCount) {
                    skip = rowCount - pageSize;
                }
                return skip;
            }
        }
    }
}
