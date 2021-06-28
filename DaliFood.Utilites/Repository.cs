
using DaliFood.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DaliFood.Utilites
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        ApplicationDbContext context;
        private DbSet<TEntity> _dbset;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            _dbset = context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "", char includesSplitChar = ',')
        {
            IQueryable<TEntity> query = _dbset;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (string include in includes.Split(includesSplitChar))
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }
        public virtual TEntity GetById(object id)
        {
            return _dbset.Find(id);
        }
        public virtual bool Create(TEntity model)
        {
            try
            {
                _dbset.Add(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public virtual bool Modifie(TEntity model)
        {
            try
            {
                if (context.Entry(model).State == EntityState.Detached)
                {                
                    _dbset.Attach(model);
                }
                context.Entry(model).State = EntityState.Modified;
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public virtual bool Delete(TEntity model)
        {
            try
            {
                if (context.Entry(model).State == EntityState.Detached)
                {
                    _dbset.Attach(model);
                }

                _dbset.Remove(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public virtual bool Delete(object id)
        {
            var entity = GetById(id);
            return Delete(entity);
        }
        public bool Save()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SaveAsync()
        {
             await context.SaveChangesAsync();
        }

        public static bool operator +(Repository<TEntity> repository, TEntity model)
        {
            return repository.Create(model);
        }
        public static bool operator -(Repository<TEntity> repository, TEntity model)
        {
            return repository.Delete(model);
        }
        public static bool operator -(Repository<TEntity> repository, object id)
        {
            return repository.Delete(id);
        }
        public static bool operator %(Repository<TEntity> repository, TEntity model)
        {
            return repository.Modifie(model);
        }


    }
}
