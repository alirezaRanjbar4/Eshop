namespace Eshop.Common.Helpers.Utilities.Utilities.Providers
{
    public static class PasswordHelper
    {
        public static bool isValidPassword(this string Password)
        {
            if (Password.Length < 8)
            {
                return false;
            }
            return true;
        }
    }
}
