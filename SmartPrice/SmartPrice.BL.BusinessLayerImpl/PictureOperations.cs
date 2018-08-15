using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.DL.DataLayerContract;
using SmartPrice.DL.DataLayerContract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.BL.BusinessLayerImpl
{
    public class PictureOperations : IPictureOperations
    {
        private IDataAccess<Picture> _pictureDataAccess;

        public PictureOperations(IDataAccess<Picture> pictureDataAccess)
        {
            _pictureDataAccess = pictureDataAccess;
        }

        public int GetNextPathId()
        {
            return _pictureDataAccess.Read().Count() + 1;
        }
    }
}
