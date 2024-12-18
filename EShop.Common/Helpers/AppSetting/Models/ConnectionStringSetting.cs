namespace Eshop.Common.Helpers.AppSetting.Models
{
    public class ConnectionStringSetting
    {


        public string? ConnectionString { get; set; }

        public string? InitialCatalog { get; set; }
        public string? ServerName { get; set; }


        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
