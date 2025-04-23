using Eshop.Share.Enum;
using Eshop.Share.Enum.EnumConverter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eshop.Presentation.Controllers.General
{
    [Authorize]
    public class EnumController : BaseController
    {
        [HttpPost(nameof(GetEnumValues))]
        public IActionResult GetEnumValues([FromBody] List<string> enumNames)
        {
            List<GetEnumsDTO> getEnums = new List<GetEnumsDTO>();
            Assembly assembly = Assembly.GetAssembly(typeof(AuthenticationResourceEnums));
            if (enumNames != null && enumNames.Count() > 0)
            {
                for (int i = 0; i < enumNames.Count(); i++)
                {
                    Type enumType = assembly.GetTypes().FirstOrDefault(t => t.IsEnum && t.Name == enumNames.ElementAt(i));

                    if (enumType != null)
                    {
                        // استفاده از EnumToListConverter برای تبدیل مقادیر enum به لیست مدل
                        var converterType = typeof(EnumToListConverter<,>).MakeGenericType(enumType, typeof(EnumModel));
                        var converter = Activator.CreateInstance(converterType);
                        var convertMethod = converterType.GetMethod("ConvertEnumToList");
                        var enumValues = convertMethod.Invoke(converter, null) as List<EnumModel>;
                        getEnums.Add(new GetEnumsDTO { EnumType = enumNames.ElementAt(i), EnumValues = enumValues });
                    }
                }
                return Ok(getEnums);
            }

            return BadRequest("Invalid enum name.");
        }
    }
}
