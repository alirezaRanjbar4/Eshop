using Eshop.Share.Helpers.AppSetting.Interfaces;

namespace Eshop.Share.Helpers.AppSetting.Models
{
    internal class PropertyData : IProperty
    {


        public string PropertyName { get; set; }

        public string? Value { get; set; }


    }
}
