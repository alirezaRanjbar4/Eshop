namespace Eshop.Share.Helpers.Utilities.Utilities.Providers
{
    public static class FileRelatedStaticPath
    {

        public static class FileTypesInDB
        {
            public static string CustomerConfirmationPhoto = "CustomerConfirmationPhoto";
            public static string CustomerFilePhoto = "CustomerFilePhoto";
            public static string CustomerOrderFile = "CustomerOrderFile";
        }

        public static class FileStatusesInDB
        {
            public static string FileReceivedInRegistrationUnit = "Input";
            public static string FileSampleInformation = "Input-Sample";
            public static string FileReceivedInDesignUnit = "Edit-Input";
            public static string FileInDesignUnit = "Edit-Edit";
            public static string FileEmailToCustomerDesignUnit = "Edit-Email";
            public static string FileConfirmedByCustomer = "Edit-Final";
            public static string FileEditedByCustomer = "Edit-Customer";


            public static string FileSentToQC = "QC";
            public static string FinalProducedFile = "Edit-Email";
            public static string BijakImage = "Bijak";
        }


        ////public static Dictionary<string, string> RelationBtwStatusUnits = new Dictionary<string, string>
        ////    {
        ////        {"Input", "سفارشات"},
        ////        {"Edit-Input", "طراحی"},
        ////        {"Edit-Edit", "طراحی در حال ویرایش"},
        ////        {"Edit-Email", "طراحی ارسال برای مشتری"},
        ////        {"Edit-Final", "طراحی تایید شده توسط مشتری"},
        ////        {"QC", "کنترل کیفی پیش از تولید"},
        ////        {"Final", "نهایی ارسال شده برای تولید"}

        ////    };

        ////// these parts must be later replaced with "GetByName"!!! 
        ////// and is rediciulous actually right now 
        ////public static Dictionary<string, long> FileTypesInDB = new Dictionary<string, long>
        ////    {
        ////        {"CustomerConfirmationPhoto", 1},
        ////        {"CustomerFilePhoto", 2},
        ////        {"CustomerOrderFile", 3}

        ////    };

        ////public static Dictionary<string, long> FileStatusesInDB = new Dictionary<string, long>
        ////    {
        ////        {"Input", 2},
        ////        {"Edit-Input", 3},
        ////        {"Edit-Edit", 4},
        ////        {"Edit-Email", 5},
        ////        {"Edit-Final", 6},
        ////        {"QC", 7},
        ////        {"Final", 8}

        ////    };

    }
}
