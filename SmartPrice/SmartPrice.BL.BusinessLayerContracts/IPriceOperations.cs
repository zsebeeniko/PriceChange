using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System.Collections.Generic;

namespace SmartPrice.BL.BusinessLayerContracts
{
    public interface IPriceOperations
    {
        IEnumerable<PriceDTO> Get();
        decimal GetExchangedValue(string priceToConvert, string to_currency);
        string GetFromCurrency(string priceToConvert);
        int GetNextPathId();
        void Create(PriceDTO priceDto);
    }
}
