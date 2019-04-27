using Microsoft.EntityFrameworkCore;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Core.Models;
using SimplCommerce.Module.GSTIndia.Models;

namespace SimplCommerce.Module.GSTIndia.Data
{
    public class TaxCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxClass>().HasData(new TaxClass(1) { Name = "Standard VAT" });

            modelBuilder.Entity<AppSetting>().HasData(
                new AppSetting("Tax.DefaultTaxClassId") { Module = "Tax", IsVisibleInCommonSettingPage = true, Value = "1" }
            );
        }
    }
}
