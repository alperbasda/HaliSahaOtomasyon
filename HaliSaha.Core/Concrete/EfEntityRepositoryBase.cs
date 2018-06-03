using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using HaliSaha.Core.Abstract;
using HaliSaha.Core.Entity;

namespace HaliSaha.Core.Concrete
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private TContext context;
        public EfEntityRepositoryBase()
        {
            if (context == null)
            {
                context = Activator.CreateInstance<TContext>();
            }   
        }
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            
                return context.Set<TEntity>().SingleOrDefault(filter);
            
        }

        public TEntity Add(TEntity entity)
        {
            
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return entity;
            
        }

        public TEntity Update(TEntity entity)
        {
            
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            
        }

        public void Delete(TEntity entity)
        {
            
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            
        }
    }
}