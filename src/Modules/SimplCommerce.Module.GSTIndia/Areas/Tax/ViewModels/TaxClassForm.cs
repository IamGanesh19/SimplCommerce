﻿using System.ComponentModel.DataAnnotations;

namespace SimplCommerce.Module.GSTIndia.Areas.Tax.ViewModels
{
    public class TaxClassForm
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Name { get; set; }

        [StringLength(8)]
        public string HSNCode { get; set; }

        [StringLength(6)]
        public string SAC { get; set; }
    }
}
