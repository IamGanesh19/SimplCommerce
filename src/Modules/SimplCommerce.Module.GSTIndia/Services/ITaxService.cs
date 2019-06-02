using SimplCommerce.Module.GSTIndia.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimplCommerce.Module.GSTIndia.Services
{
    public interface ITaxService
    {
        //Task<decimal> GetTaxPercent(long? taxClassId, string countryId, long stateOrProvinceId, decimal? Price);

        Task<IList<TaxRate>> GetTaxRates(long? taxClassId, string countryId, long stateOrProvinceId, decimal? Price);
    }
}
