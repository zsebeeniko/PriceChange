using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.DL.DataLayerContract;

namespace SmartPrice.BL.BusinessLayerImpl
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository _repository;
        private IProductOperations _productOperations;
        private IPriceOperations _priceOperations;
        private IMarkerOperations _markerOperations;
        private IPictureOperations _pictureOperations;

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
                    _priceOperations = new PriceOperations(_repository.PriceRepository, _repository.ProductRepository);
                }

                return _priceOperations;
            }
        }

        public IMarkerOperations MarkerOperations
        {
            get
            {
                if (_markerOperations == null)
                {
                    _markerOperations = new MarkerOperations(_repository.MarkerRepository);
                }

                return _markerOperations;
            }
        }
        public IPictureOperations PictureOperations
        {
            get
            {
                if (_pictureOperations == null)
                {
                    _pictureOperations = new PictureOperations(_repository.PictureRepository);
                }

                return _pictureOperations;
            }
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
