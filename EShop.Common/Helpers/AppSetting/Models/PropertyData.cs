using Eshop.Common.Helpers.AppSetting.Interfaces;

namespace Eshop.Common.Helpers.AppSetting.Models
{
    internal class PropertyData : IProperty
    {


        public string PropertyName { get; set; }

        public string? Value { get; set; }


    }
}
