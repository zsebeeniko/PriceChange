using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.BL.BusinessLayerContracts
{
    public interface IProductPriceOperations
    {
        void Create(ProductPriceDTO productPrice);
        IEnumerable<ProductPriceDTO> Get();
    }
}
