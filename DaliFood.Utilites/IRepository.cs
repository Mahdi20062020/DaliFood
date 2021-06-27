using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DaliFood.Utilites
{
    internal interface IRepository<TEntity>
    {
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "", char includesSplitChar = ',');
        public TEntity GetById(object id);
        public bool Create(TEntity model);
        public bool Modifie(TEntity model);
        public bool Delete(TEntity model);
        public bool Delete(object id);
        public bool Save();
        public Task SaveAsync();
    }
}
