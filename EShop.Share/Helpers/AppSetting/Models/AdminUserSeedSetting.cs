namespace Eshop.Share.Helpers.AppSetting.Models
{
    public class AdminUserSeedSetting : BaseSettingModel<AdminUserSeedSetting>
    {
        public AdminUserSeedSetting()
        {
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
