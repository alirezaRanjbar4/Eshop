using System;

namespace Eshop.Share.Helpers.AppSetting.Models
{
    public abstract class BaseSettingModel<T>
    {
        Type t = typeof(T);
        public string Name { get; set; }

        public BaseSettingModel()
        {
        }
    }
}
