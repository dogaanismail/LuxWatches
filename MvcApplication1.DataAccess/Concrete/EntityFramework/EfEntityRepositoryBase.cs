﻿using MvcApplication1.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MvcApplication1.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {

        public List<TEntity> getList()
        {
            using (TContext context= new TContext())
            {
                return context.Set<TEntity>().ToList();
            }
        }

        public void Add(TEntity entity)
        {
            using (TContext context= new TContext())
            {
                var addedEntitiy = context.Entry(entity);
                addedEntitiy.State = EntityState.Added;
                context.SaveChanges();               
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntitiy = context.Entry(entity);
                deletedEntitiy.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<TEntity> List<F>(Expression<Func<TEntity, F>> where=null)
        {
            using (TContext context= new TContext())
            {
                return where==null?
                    context.Set<TEntity>().ToList():
                    context.Set<TEntity>().OrderByDescending(where).ToList();
            }
        }

        public List<TEntity> Query(Expression<Func<TEntity, bool>> where=null)
        {
            using (TContext context= new TContext())
            {
                return context.Set<TEntity>().Where(where).ToList();
            }
        }

        public List<TEntity> Include(string includeTable)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().Include(includeTable).ToList();

            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().Where(filter).FirstOrDefault();
            }
        }

        public virtual List<TEntity> GetAllLazyLoad(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] children)
        {
            
             using (TContext context = new TContext())
            {
                var query = context.Set<TEntity>().ToList();
                foreach (var child in children)
                    query = context.Set<TEntity>().Include(child).Where(filter).ToList(); 
                return query.ToList();
             }
        }


        public int Add2(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntitiy = context.Entry(entity);
                addedEntitiy.State = EntityState.Added;
                return context.SaveChanges();
            }
            
        }

    }
}
