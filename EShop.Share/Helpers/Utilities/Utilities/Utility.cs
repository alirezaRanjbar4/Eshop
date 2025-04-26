using Eshop.Share.Helpers.Utilities.Interface;
using Eshop.Share.Helpers.Utilities.Utilities.Providers;

namespace Eshop.Share.Helpers.Utilities.Utilities
{
    public class Utility : IUtility
    {
        #region Property

        private ICurrentUserProvider _currentUser;
        private static ICurrentUserProvider _currentUserStatic;

        /// <summary>
        /// کلاس مربوط به کا با تقویم
        /// </summary>
        private static CalandarHelper? _calanderHelper;
        /// <summary>
        /// کلاس مربوط به کار با آبجکت ها 
        /// </summary>
        private static ObjectHelper? _objectHelper;
        /// <summary>
        /// کلاس مربوط به تنطیمات سیستم جاری
        /// مثل:کاربرجاری وغیره
        /// </summary>
        private static SystemSettingsHelper? _systemSettingsHelper;
        /// <summary>
        /// کلاس مربوط به آدرس دهی ها
        /// </summary>
        private static PathHelper? _PathHelper;
        /// <summary>
        /// کلاس مربوط به تبدیل واحد ها
        /// </summary>
        private static ConvertorHelper? _convertorHelper;

        private static SecurityHelper? _securityHelper;

        private static EnumHelper? _enumHelper;

        private static ExcelHelper? _excelHelper;

        #endregion

        //public Utility()
        //{
        //}
        public Utility(ICurrentUserProvider currentUser)
        {
            _currentUser = currentUser;
            _currentUserStatic = _currentUser;
        }


        #region Public GetInstanceOfUtility
        public static CalandarHelper CalandarProvider
        {
            get
            {
                if (_calanderHelper == null)
                {
                    _calanderHelper = new CalandarHelper(_currentUserStatic.Culture);
                }
                return _calanderHelper;
            }
        }

        public static ObjectHelper ObjectProvider
        {
            get
            {
                if (_objectHelper == null)
                {
                    _objectHelper = new ObjectHelper();
                }
                return _objectHelper;
            }
        }

        public static SecurityHelper SecurityHelper
        {
            get
            {
                if (_securityHelper == null)
                {
                    _securityHelper = new SecurityHelper();
                }
                return _securityHelper;
            }
        }

        public static EnumHelper EnumHelper
        {
            get
            {
                if (_enumHelper == null)
                {
                    _enumHelper = new EnumHelper();
                }
                return _enumHelper;
            }
        }

        public static SystemSettingsHelper SystemSettingsProvider
        {
            get
            {
                if (_systemSettingsHelper == null)
                {
                    _systemSettingsHelper = new SystemSettingsHelper(currentUser: _currentUserStatic);
                }
                return _systemSettingsHelper;
            }
        }

        public static PathHelper PathProvider
        {
            get
            {
                if (_PathHelper == null)
                {
                    _PathHelper = new PathHelper();
                }
                return _PathHelper;
            }
        }

        public static ConvertorHelper ConvertorProvider
        {
            get
            {
                if (_convertorHelper == null)
                {
                    _convertorHelper = new ConvertorHelper();
                }
                return _convertorHelper;
            }
        }


        public static ExcelHelper ExcelProvider
        {
            get
            {
                if (_excelHelper == null)
                {
                    _excelHelper = new ExcelHelper();
                }
                return _excelHelper;
            }
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
