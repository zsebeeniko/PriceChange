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
    public class MarkerOperations : IMarkerOperations
    {
        private IDataAccess<Marker> _markerDataAccess;

        public MarkerOperations(IDataAccess<Marker> markerDataAccess)
        {
            _markerDataAccess = markerDataAccess;
        }
    }
}
