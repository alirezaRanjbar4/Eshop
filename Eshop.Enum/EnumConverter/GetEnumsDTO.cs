using System.Collections.Generic;

namespace Eshop.Enum.EnumConverter
{
    public class GetEnumsDTO
    {
        public string EnumType { get; set; }
        public List<EnumModel> EnumValues { get; set; }
    }
}
