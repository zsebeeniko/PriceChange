using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice.DL.DataLayerContract.Entities;

namespace SmartPrice.DL.DataLayerContract
{
    public interface IDataAccess<T>
        where T : IEntity
    {
        IQueryable<T> Read();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
