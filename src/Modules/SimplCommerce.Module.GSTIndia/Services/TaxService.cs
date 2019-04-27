using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.GSTIndia.Models;

namespace SimplCommerce.Module.GSTIndia.Services
{
    public class TaxService : ITaxService
    {
        private readonly IRepository<TaxRate> _taxRateRepository;

        public TaxService(IRepository<TaxRate> taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public async Task<decimal> GetTaxPercent(long? taxClassId, string countryId, long stateOrProvinceId, string zipCode, decimal? Price)
        {
            if (!taxClassId.HasValue)
            {
                return 0;
            }

            var query = _taxRateRepository.Query()
                           .Where(x => x.CountryId == countryId
                           && ((x.StateOrProvinceId != null && x.StateOrProvinceId == stateOrProvinceId) || x.StateOrProvinceId == null)
                           && x.TaxClassId == taxClassId.Value);
            if (!string.IsNullOrEmpty(zipCode))
            {
                query = query.Where(x => (x.ZipCode != null && x.ZipCode == zipCode) || x.ZipCode == null );
            }

            if (Price != null)
            {
                query = query.Where(x => (!x.MinPriceRange.HasValue || x.MinPriceRange.Value <= Price.Value) && (!x.MaxPriceRange.HasValue || x.MaxPriceRange.Value >= Price.Value));
            }

            var taxRate = await query.FirstOrDefaultAsync(); 
            if (taxRate != null)
            {
                return taxRate.Rate;
            }

            return 0;
        }
    }
}
