using Eshop.Share.Helpers.AppSetting.Models;
using System.Linq;

namespace Eshop.Share.Helpers.AppSetting.Mapper
{
    public class AppSettingData
    {
        private readonly AppSettingHelper _appSettingHelper;

        public JWTSettings? JWTSettings { get; set; }
        public BaseSetting? BaseSetting { get; set; }
        public AppInfoSetting AppInfoSetting { get; set; }
        public EmailSetting EmailSetting { get; set; }
        public OrderSetting OrderSetting { get; set; }
        public AdminUserSeedSetting AdminUserSeedSetting { get; set; }
        public MongoSetting MongoSetting { get; set; }
        public SmtpConfigSetting SmtpConfigSetting { get; set; }


        public AppSettingData()
        {
            _appSettingHelper = AppSettingHelper.Instance(null);

            JWTSettings = JwtSettingsSet();
            BaseSetting = BaseSettingSet();
            AppInfoSetting = AppInfoSettingsSet();
            EmailSetting = EmailSettingsSet();
            OrderSetting = OrderSettingsSet();
            AdminUserSeedSetting = AdminUserSeedSettingSet();
            MongoSetting = MongoSettingSet();
            SmtpConfigSetting = SmtpConfigSettingSet();
        }

        /// <summary>
        /// دریافت ادرس از Appsetting
        /// </summary>
        /// <returns></returns>
        private BaseSetting BaseSettingSet()
        {
            var data = _appSettingHelper.GetValue<BaseSetting>();
            var model = new BaseSetting
            {
                EndpointAddress = data.FirstOrDefault(x => x.PropertyName == nameof(BaseSetting.EndpointAddress))?.Value
            };

            return model;
        }

        /// <summary>
        /// داده هاي مربوط به JWT
        /// </summary>
        /// <returns></returns>
        private JWTSettings JwtSettingsSet()
        {
            var data = _appSettingHelper.GetValue<JWTSettings>();
            var model = new JWTSettings
            {
                AccessTokenSecret = data.FirstOrDefault(x => x.PropertyName == nameof(JWTSettings.AccessTokenSecret))?.Value,
                RefreshTokenSecret = data.FirstOrDefault(x => x.PropertyName == nameof(JWTSettings.RefreshTokenSecret))?.Value,
                AccessTokenExpirationMinutes = data.FirstOrDefault(x => x.PropertyName == nameof(JWTSettings.AccessTokenExpirationMinutes))?.Value,
                RefreshTokenExpirationMinutes = data.FirstOrDefault(x => x.PropertyName == nameof(JWTSettings.RefreshTokenExpirationMinutes))?.Value,
                Issuer = data.FirstOrDefault(x => x.PropertyName == nameof(JWTSettings.Issuer))?.Value,
                Audience = data.FirstOrDefault(x => x.PropertyName == nameof(JWTSettings.Audience))?.Value,
                SecretKey = data.FirstOrDefault(x => x.PropertyName == nameof(JWTSettings.SecretKey))?.Value,
                EncryptKey = data.FirstOrDefault(x => x.PropertyName == nameof(JWTSettings.EncryptKey))?.Value,
                NotBeforeMinutes = data.FirstOrDefault(x => x.PropertyName == nameof(JWTSettings.NotBeforeMinutes))?.Value,
                ExpirationMinutes = data.FirstOrDefault(x => x.PropertyName == nameof(JWTSettings.ExpirationMinutes))?.Value
            };

            return model;
        }

        /// <summary>
        /// داده هاي مربوط به AppInfo
        /// </summary>
        /// <returns></returns>
        private AppInfoSetting AppInfoSettingsSet()
        {
            var data = _appSettingHelper.GetValue<AppInfoSetting>();
            var model = new AppInfoSetting
            {
                Title = data.FirstOrDefault(x => x.PropertyName == nameof(AppInfoSetting.Title))?.Value,
                Description = data.FirstOrDefault(x => x.PropertyName == nameof(AppInfoSetting.Description))?.Value,
                Logo = data.FirstOrDefault(x => x.PropertyName == nameof(AppInfoSetting.Logo))?.Value,
                Favicon = data.FirstOrDefault(x => x.PropertyName == nameof(AppInfoSetting.Favicon))?.Value,
                MetaDescriptionTag = data.FirstOrDefault(x => x.PropertyName == nameof(AppInfoSetting.MetaDescriptionTag))?.Value,
                Url = data.FirstOrDefault(x => x.PropertyName == nameof(AppInfoSetting.Url))?.Value,
                FileStorageUrl = data.FirstOrDefault(x => x.PropertyName == nameof(AppInfoSetting.FileStorageUrl))?.Value
            };

            return model;
        }

        /// <summary>
        /// داده هاي مربوط به EmailSetting
        /// </summary>
        /// <returns></returns>
        /// 
        private EmailSetting EmailSettingsSet()
        {
            var data = _appSettingHelper.GetValue<EmailSetting>();
            var model = new EmailSetting
            {
                Username = data.FirstOrDefault(x => x.PropertyName == nameof(EmailSetting.Username))?.Value,
                Host = data.FirstOrDefault(x => x.PropertyName == nameof(EmailSetting.Host))?.Value,
                Password = data.FirstOrDefault(x => x.PropertyName == nameof(EmailSetting.Password))?.Value,
                Email = data.FirstOrDefault(x => x.PropertyName == nameof(EmailSetting.Email))?.Value,
                Port = data.FirstOrDefault(x => x.PropertyName == nameof(EmailSetting.Port))?.Value
            };

            return model;
        }

        /// <summary>
        /// داده هاي مربوط به OrderSetting
        /// </summary>
        /// <returns></returns>
        /// 
        private OrderSetting OrderSettingsSet()
        {
            var data = _appSettingHelper.GetValue<OrderSetting>();
            var model = new OrderSetting
            {
                OrderNumberCounterStart = data.FirstOrDefault(x => x.PropertyName == nameof(OrderSetting.OrderNumberCounterStart))?.Value
            };

            return model;
        }

        /// <summary>
        /// داده هاي مربوط به AdminUserSeedSetting
        /// </summary>
        /// <returns></returns>
        /// 
        private AdminUserSeedSetting AdminUserSeedSettingSet()
        {
            var data = _appSettingHelper.GetValue<AdminUserSeedSetting>();
            var model = new AdminUserSeedSetting
            {
                Username = data.FirstOrDefault(x => x.PropertyName == nameof(AdminUserSeedSetting.Username))?.Value,
                Password = data.FirstOrDefault(x => x.PropertyName == nameof(AdminUserSeedSetting.Password))?.Value,
                Email = data.FirstOrDefault(x => x.PropertyName == nameof(AdminUserSeedSetting.Email))?.Value,
                RoleName = data.FirstOrDefault(x => x.PropertyName == nameof(AdminUserSeedSetting.RoleName))?.Value,
                FirstName = data.FirstOrDefault(x => x.PropertyName == nameof(AdminUserSeedSetting.FirstName))?.Value,
                LastName = data.FirstOrDefault(x => x.PropertyName == nameof(AdminUserSeedSetting.LastName))?.Value
            };

            return model;
        }

        /// <summary>
        /// داده هاي مربوط به MongoSetting
        /// </summary>
        /// <returns></returns>
        /// 
        private MongoSetting MongoSettingSet()
        {
            var data = _appSettingHelper.GetValue<MongoSetting>();
            var model = new MongoSetting
            {
                ConnectionString = data.FirstOrDefault(x => x.PropertyName == nameof(MongoSetting.ConnectionString))?.Value,
                DatabaseName = data.FirstOrDefault(x => x.PropertyName == nameof(MongoSetting.DatabaseName))?.Value,
                Collection = data.FirstOrDefault(x => x.PropertyName == nameof(MongoSetting.Collection))?.Value,
                ExpireDocumentTime = data.FirstOrDefault(x => x.PropertyName == nameof(MongoSetting.ExpireDocumentTime))?.Value,
                ExpireCollectionTime = data.FirstOrDefault(x => x.PropertyName == nameof(MongoSetting.ExpireCollectionTime))?.Value
            };

            return model;
        }

        /// <summary>
        /// داده هاي مربوط به MongoSetting
        /// </summary>
        /// <returns></returns>
        /// 
        private SmtpConfigSetting SmtpConfigSettingSet()
        {
            var data = _appSettingHelper.GetValue<SmtpConfigSetting>();
            var model = new SmtpConfigSetting
            {
                From = data.FirstOrDefault(x => x.PropertyName == nameof(SmtpConfigSetting.From))?.Value,
                FromAlias = data.FirstOrDefault(x => x.PropertyName == nameof(SmtpConfigSetting.FromAlias))?.Value,
                SmtpServer = data.FirstOrDefault(x => x.PropertyName == nameof(SmtpConfigSetting.SmtpServer))?.Value,
                Port = data.FirstOrDefault(x => x.PropertyName == nameof(SmtpConfigSetting.Port))?.Value,
                Username = data.FirstOrDefault(x => x.PropertyName == nameof(SmtpConfigSetting.Username))?.Value,
                Password = data.FirstOrDefault(x => x.PropertyName == nameof(SmtpConfigSetting.Password))?.Value
            };

            return model;
        }
    }
}