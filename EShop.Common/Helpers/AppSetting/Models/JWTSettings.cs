namespace Eshop.Common.Helpers.AppSetting.Models
{
    public class JWTSettings : BaseSettingModel<JWTSettings>
    {
        public JWTSettings()
        { }
        public string AccessTokenSecret { get; set; }
        public string RefreshTokenSecret { get; set; }
        public string AccessTokenExpirationMinutes { get; set; }
        public string RefreshTokenExpirationMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public string EncryptKey { get; set; }
        public string NotBeforeMinutes { get; set; }
        public string ExpirationMinutes { get; set; }
    }

}

