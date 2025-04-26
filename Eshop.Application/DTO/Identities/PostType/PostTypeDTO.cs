using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Identities.PostType
{
    public class PostTypeDTO : BaseDTO
    {
        /// <summary>
        /// عنوان
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کد
        /// </summary>
        public string Code { get; set; }
        public string PersianName { get; set; }
    }
}
