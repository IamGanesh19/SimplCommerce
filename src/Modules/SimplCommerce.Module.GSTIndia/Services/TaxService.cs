using System.Collections.Generic;
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

        //public async Task<decimal> GetTaxPercent(long? taxClassId, string countryId, long stateOrProvinceId, decimal? Price)
        //{
        //    if (!taxClassId.HasValue)
        //    {
        //        return 0;
        //    }

        //    var query = _taxRateRepository.Query()
        //                   .Where(x => x.CountryId == countryId
        //                   && ((x.StateOrProvinceId != null && x.StateOrProvinceId == stateOrProvinceId) || x.StateOrProvinceId == null)
        //                   && x.TaxClassId == taxClassId.Value);

        //    if (Price != null)
        //    {
        //        query = query.Where(x => (!x.MinPriceRange.HasValue || x.MinPriceRange.Value <= Price.Value) && (!x.MaxPriceRange.HasValue || x.MaxPriceRange.Value >= Price.Value));
        //    }

        //    var taxRates = await query.ToListAsync();
        //    decimal rateofTax = 0;
        //    if (taxRates != null && taxRates.Count > 0)
        //    {
        //        foreach (var taxRate in taxRates)
        //        {
        //            rateofTax += taxRate.Rate;
        //        }
        //        return rateofTax;
        //    }

        //    return 0;
        //}

        public async Task<IList<TaxRate>> GetTaxRates(long? taxClassId, string countryId, long stateOrProvinceId, decimal? Price)
        {
            if (!taxClassId.HasValue)
            {
                return null;
            }

            var query = _taxRateRepository.Query()
                           .Where(x => x.CountryId == countryId
                           && ((x.StateOrProvinceId != null && x.StateOrProvinceId == stateOrProvinceId) || x.StateOrProvinceId == null)
                           && x.TaxClassId == taxClassId.Value);

            if (Price != null)
            {
                query = query.Where(x => (!x.MinPriceRange.HasValue || x.MinPriceRange.Value <= Price.Value) && (!x.MaxPriceRange.HasValue || x.MaxPriceRange.Value >= Price.Value));
            }

            return await query.ToListAsync();
        }
    }
}
