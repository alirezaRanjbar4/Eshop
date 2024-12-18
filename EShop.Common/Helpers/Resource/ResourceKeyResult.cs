using Eshop.Enum;
using System.Collections.Generic;

namespace Eshop.Common.Helpers.Resource
{
    public class ResourceKeyResult
    {
        public ResultType ResultType { get; set; }
        public List<ResourceKey> ResourceKeyList { get; set; }


        public ResourceKeyResult(ResultType resultType = ResultType.Warning, System.Enum enumValue = null)
        {
            ResultType = resultType;
            ResourceKeyList = ResourceKeyList ?? new List<ResourceKey>();

            if (enumValue != null)
            {
                ResourceKey resultResourceKey = new ResourceKey(enumValue.GetType().Name, enumValue.ToString());
                ResourceKeyList.Add(resultResourceKey);
            }

        }
    }
}
