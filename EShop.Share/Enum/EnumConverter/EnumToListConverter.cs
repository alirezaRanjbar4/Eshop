using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Eshop.Share.Enum.EnumConverter
{
    public class EnumToListConverter<TEnum, TModel>
    where TEnum : System.Enum
    where TModel : IEnumModel, new()
    {
        public List<TModel> ConvertEnumToList()
        {
            var enumValues = System.Enum.GetValues(typeof(TEnum));

            var result = new List<TModel>();

            foreach (var enumValue in enumValues)
            {
                if (System.Enum.IsDefined(typeof(TEnum), enumValue))
                {
                    var model = new TModel
                    {
                        Id = Convert.ToInt32(enumValue),
                        Name = enumValue.ToString(),
                        Description = GetEnumDescription((TEnum)enumValue)
                    };

                    result.Add(model);
                }
            }

            return result;
        }

        private string GetEnumDescription<TEnum>(TEnum value) where TEnum : System.Enum
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo != null)
            {
                var descriptionAttribute = (DescriptionAttribute)fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

                if (descriptionAttribute != null)
                    return descriptionAttribute.Description;
            }

            return null;
        }
    }
}
