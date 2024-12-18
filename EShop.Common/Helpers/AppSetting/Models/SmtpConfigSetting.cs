using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Common.Helpers.AppSetting.Models
{
    public class SmtpConfigSetting : BaseSettingModel<SmtpConfigSetting>
    {
        public SmtpConfigSetting()
        {
        }
        public string From { get; set; }
        public string FromAlias { get; set; }
        public string SmtpServer { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

