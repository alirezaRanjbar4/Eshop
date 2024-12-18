using Eshop.DTO.General;

namespace Eshop.DTO.Identities.PostType
{
    public class PostTypeDTO : BaseDto
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
