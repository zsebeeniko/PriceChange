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

        IMarkerOperations MarkerOperations { get; }

        IPictureOperations PictureOperations { get; }

        void SaveChanges();
    }
}
