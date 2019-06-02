using SimplCommerce.Module.GSTIndia.Models;
using System.ComponentModel.DataAnnotations;

namespace SimplCommerce.Module.GSTIndia.Areas.Tax.ViewModels
{
    public class TaxRateForm
    {
        public long Id { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Tax Class is required")]
        public long TaxClassId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string CountryId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public decimal Rate { get; set; }

        public long? StateOrProvinceId { get; set; }

        public string ZipCode { get; set; }

        public decimal? MinPriceRange { get; set; }

        public decimal? MaxPriceRange { get; set; }

        public string TaxType { get; set; }
    }
}
