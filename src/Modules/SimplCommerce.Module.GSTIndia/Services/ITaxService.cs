using System.Threading.Tasks;

namespace SimplCommerce.Module.GSTIndia.Services
{
    public interface ITaxService
    {
        Task<decimal> GetTaxPercent(long? taxClassId, string countryId, long stateOrProvinceId, string ZipCode, decimal? Price);
    }
}
