using SmartPrice.DL.DataLayerContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice.DL.DataLayerContract.Entities;
using System.Data.Entity;

namespace SmartPrice.DL.EFDataLayer
{
    public class EFDataAccess<T> : IDataAccess<T>
        where T : class, IEntity
    {
        private DbSet<T> _dbSet;

        public EFDataAccess(SmartPriceContext ctx)
        {
            _dbSet = ctx.Set<T>();
        }

        public IQueryable<T> Read()
        {
            return _dbSet;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
