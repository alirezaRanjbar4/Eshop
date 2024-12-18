using Eshop.DTO.General;

namespace Eshop.DTO.Identities.Post
{
    public class PostDTO : BaseDto
    {
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کد
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// شناسه رده سازمانی
        /// </summary>
        public Guid PostTypeId { get; set; }

        public ICollection<Guid> Roles { get; set; }
    }
}
