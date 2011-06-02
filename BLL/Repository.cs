using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public abstract class Repository<TEntity, TKey> where TEntity: class
    {
        protected Context Context { get; private set; }

        public Repository()
        {
            Context = new Context();
        }

        public TEntity Get(TKey id) 
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IList<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsNoTracking().ToList();
        }

        public void Add(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Added;

            Context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;

            Context.SaveChanges();
        }

        public virtual void Delete(TKey id)
        {
            if (IsExists(id)) {
                Context.Set<TEntity>().Remove(Get(id));

                Context.SaveChanges();
            }
        }

        public bool IsExists(TKey id) 
        {
            return null != Get(id);
        }

        public virtual IList<TEntity> PageOf(int pageIndex, int pageSize, out int rowCount) 
        {
            using (Context db=new Context()) {
                int skipCount = pageSize * (pageIndex - 1);
                rowCount = db.Set<TEntity>().Count();
                var query = db.Set<TEntity>().AsNoTracking().Skip(skipCount).Take(pageSize);
                return query.ToList();
            }
        }

       
    }
}
