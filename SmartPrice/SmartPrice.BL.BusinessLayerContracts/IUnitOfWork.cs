using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.BL.BusinessLayerContracts
{
    public interface IUnitOfWork
    {
        IProductOperations ProductOperations { get; }

        IPriceOperations PriceOperations { get; }

        IProductPriceOperations ProductPriceOperations { get; }

        void SaveChanges();
    }
}
