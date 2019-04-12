using Microsoft.EntityFrameworkCore;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Core.Models;

namespace SimplCommerce.Module.ShipmentTrackingAfterShip.Data
{
    public class ShipmentTrackerAfterShipCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppSetting>().HasData(
                new AppSetting("Aftership.ApiKey") { Module = "ShipmentTrackingAfterShip", IsVisibleInCommonSettingPage = false, Value = "" }
            );
        }
    }
}
