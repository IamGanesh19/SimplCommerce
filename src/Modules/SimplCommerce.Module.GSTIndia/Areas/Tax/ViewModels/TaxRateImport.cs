using System.ComponentModel.DataAnnotations;

namespace SimplCommerce.Module.GSTIndia.Areas.Tax.ViewModels
{
    public class TaxRateImport
    {
        [Range(1, long.MaxValue, ErrorMessage = "Tax Class is required")]
        public long TaxClassId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string CountryId { get; set; }

        public string StateOrProvinceName { get; set; }

        public string ZipCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public decimal Rate { get; set; }

        public decimal? MinPriceRange { get; set; }

        public decimal? MaxPriceRange { get; set; }
    }
}
