namespace Eshop.Share.Helpers.AppSetting.Models
{
    public class MongoSetting : BaseSettingModel<MongoSetting>
    {
        public MongoSetting()
        {
        }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Collection { get; set; }
        public string ExpireDocumentTime { get; set; }
        public string ExpireCollectionTime { get; set; }
    }
}


