namespace Eshop.Share.Helpers.AppSetting.Models
{
    public class EmailSetting : BaseSettingModel<EmailSetting>
    {
        public EmailSetting()
        {
        }
        public string Username { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Port { get; set; }
    }
}

