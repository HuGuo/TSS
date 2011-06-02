using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TSS.Models;
namespace TSS.BLL
{
    public abstract class Repository<TEntity,KeyType> where TEntity:class
    {
        public Repository() { }
        public virtual void Add(TEntity entity) 
        {
            using (Context db=new Context()) {
                db.Entry<TEntity>(entity).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        public virtual void Delete(KeyType id) 
        {
            using (Context db=new Context()) {
                IDbSet<TEntity> dbset = db.Set<TEntity>();
                TEntity entity = dbset.Find(id);
                if (null !=entity) {
                    dbset.Remove(entity);
                    db.SaveChanges();
                }
            }
        }

        public virtual bool Exists(KeyType id) 
        {
            return null != Get(id);
        }

        public virtual TEntity Get(KeyType id) 
        {
            using (Context db = new Context()) {
                TEntity entity = db.Set<TEntity>().Find(id);
                return entity;
            }
        }

        public virtual void Update(TEntity entity) 
        {
            using (Context db=new Context()) {
                //IDbSet<TEntity> dbSet = db.Set<TEntity>();
                //dbSet.Attach(entity);
                db.Entry<TEntity>(entity).State = EntityState.Modified;
                db.SaveChanges();
                
            }
        }

        public virtual IList<TEntity> PageOf(int pageIndex,int pageSize,out int rowCount) 
        {
            using (Context db=new Context()) {
                int skipCount = pageSize * (pageIndex - 1);
                rowCount = db.Set<TEntity>().Count();
                var query = db.Set<TEntity>().AsNoTracking().Skip(skipCount).Take(pageSize);
                return query.ToList();
            }
        }

        public virtual IList<TEntity> GetAll() 
        {
            using (Context db=new Context()) {
                return db.Set<TEntity>().AsNoTracking().ToList();
            }
        }
    }
}
