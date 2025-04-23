namespace Eshop.Share.Enum
{
    public enum AuthenticationResourceEnums
    {
        PasswordCannotEmpty = 1,
    }

    public enum DataTypes
    {
        UniqueIdentifier,
        Bit,
        Date,
        Datetime,
        Time,
        Nvarchar,
        Varchar,
        Char,
        Image,
        Real,
        Int,
        Binary,
        Varbinary,
        Tinyint,
        Float,
        Decimal,
        Smallint,
        BigInt
    }

    public enum DbSchema
    {
        Identity,
        Model
    }

    public enum ServiceStatus
    {
        Ok,
        BadRequest,
        NotFound
    }

    public enum User
    {
        DuplicateUserName = 1,
        PasswordRequiresNonAlphanumeric = 2,
        PasswordRequiresDigit = 3,
        PasswordRequiresLower = 4,
        PasswordRequiresUpper = 5,
        PasswordTooShort = 6,
        InvalidUserName = 7,
        DuplicateEmail = 8,
        DuplicateRoleName = 9,
        PasswordMismatch = 10,
        IncorrectPassword = 11,
        IncorrectPasswordOrDuplicatePassword = 12,
        UserIsNotInTheProductionUnit = 13,
    }

    public enum UserCulture
    {
        Fa = 1,
        En = 2
    }

    public enum CalendarType
    {
        Jalali = 1,
        Gregorian = 2,
    }

    public enum LanguageType
    {
        fa_IR = 1,
        en_US = 2
    }
}
