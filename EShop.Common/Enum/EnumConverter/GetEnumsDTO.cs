using System.Collections.Generic;

namespace Eshop.Common.Enum.EnumConverter
{
    public class GetEnumsDTO
    {
        public string EnumType { get; set; }
        public List<EnumModel> EnumValues { get; set; }
    }
}
