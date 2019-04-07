using Microsoft.EntityFrameworkCore;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Core.Models;

namespace SimplCommerce.Module.EmailSenderSmtp.Data
{
    public class EmailSenderMsg91CustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppSetting>().HasData(
                new AppSetting("Msg91.Email.ApiKey") { Module = "Msg91", IsVisibleInCommonSettingPage = false, Value = "" },
                new AppSetting("Msg91.Email.ApiURL") { Module = "Msg91", IsVisibleInCommonSettingPage = false, Value = "http://control.msg91.com/api/sendmail.php" },
                new AppSetting("Msg91.Email.From") { Module = "Msg91", IsVisibleInCommonSettingPage = false, Value = "" }
            );
        }
    }
}
