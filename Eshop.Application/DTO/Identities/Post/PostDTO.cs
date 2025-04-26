using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Identities.Post
{
    public class PostDTO : BaseDTO
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
