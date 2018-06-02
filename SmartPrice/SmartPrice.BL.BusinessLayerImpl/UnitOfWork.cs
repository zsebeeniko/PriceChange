using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.DL.DataLayerContract;

namespace SmartPrice.BL.BusinessLayerImpl
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository _repository;
        private IProductOperations _productOperations;
        private IPriceOperations _priceOperations;
        private IProductPriceOperations _productPriceOperations;

        public UnitOfWork(IRepository repository)
        {
            _repository = repository;
        }

        public IProductOperations ProductOperations
        {
            get
            {
                if (_productOperations == null)
                {
                    _productOperations = new BusinessLayerImpl.ProductOperations(_repository.ProductRepository);
                }

                return _productOperations;
            }
        }

        public IPriceOperations PriceOperations
        {
            get
            {
                if (_priceOperations == null)
                {
                    _priceOperations = new PriceOperations(_repository.PriceRepository);
                }

                return _priceOperations;
            }
        }

        public IProductPriceOperations ProductPriceOperations
        {
            get
            {
                if (_productPriceOperations == null)
                {
                    _productPriceOperations = new BusinessLayerImpl.ProductPriceOperations(
                        _repository.ProductPriceRepository,
                        _repository.ProductRepository,
                        _repository.PriceRepository);
                }

                return _productPriceOperations;
            }
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
