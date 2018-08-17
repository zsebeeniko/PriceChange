using SmartPrice.DL.DataLayerContract;
using System.Linq;
using SmartPrice.DL.DataLayerContract.Entities;
using System.Data.Entity;
using SmartPrice.DL.EFDataLayer.Models;

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
