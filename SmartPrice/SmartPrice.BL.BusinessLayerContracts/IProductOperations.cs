using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.BL.BusinessLayerContracts
{
    public interface IProductOperations
    {
        int Create(ProductDTO product);
        IEnumerable<ProductDTO> Get();
        void Delete(ProductDTO product);
        List<String> GetNames();
    }
}
