using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Common.Helpers.AppSetting.Models
{
    public class OrderSetting : BaseSettingModel<OrderSetting>
    {
        public OrderSetting()
        {

        }
        public string OrderNumberCounterStart { get; set; }

    }
}

