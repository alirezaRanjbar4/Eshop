using AutoMapper;
using Eshop.Entity.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Eshop.Service.Identity.Base
{
    public class ApplicationUserManager : UserManager<UserEntity>, IApplicationUserManager
    {
        private readonly ApplicationIdentityErrorDescriber _errors;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationUserManager> _logger;
        private readonly IOptions<IdentityOptions> _options;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;
        private readonly IEnumerable<IPasswordValidator<UserEntity>> _passwordValidators;
        private readonly IServiceProvider _services;
        private readonly IUserStore<UserEntity> _userStore;
        private readonly IEnumerable<IUserValidator<UserEntity>> _userValidators;
        private readonly IMapper _mapper;

        public ApplicationUserManager(
            ApplicationIdentityErrorDescriber errors,
            ILookupNormalizer keyNormalizer,
            ILogger<ApplicationUserManager> logger,
            IOptions<IdentityOptions> options,
            IPasswordHasher<UserEntity> passwordHasher,
            IEnumerable<IPasswordValidator<UserEntity>> passwordValidators,
            IServiceProvider services,
            IUserStore<UserEntity> userStore,
            IEnumerable<IUserValidator<UserEntity>> userValidators,
            IMapper mapper)
            : base(userStore, options, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _userStore = userStore;
            _errors = errors;
            _logger = logger;
            _services = services;
            _passwordHasher = passwordHasher;
            _userValidators = userValidators;
            _options = options;
            _keyNormalizer = keyNormalizer;
            _passwordValidators = passwordValidators;
            _mapper = mapper;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            return await Users.ToListAsync();
        }

        //public async Task<List<UsersViewModel>> GetAllUsersWithRolesAsync()
        //{
        //    return await Users.Select(user => new UsersViewModel
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        UserName = user.UserName,
        //        PhoneNumber = user.PhoneNumber,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        BirthDate = user.BirthDate,
        //        IsActive = user.IsActive,
        //        Image = user.Image,
        //        RegisterDateTime = user.RegisterDateTime,
        //        Roles = user.Roles,

        //    }).ToListAsync();
        //}

        //public async Task<UsersViewModel> FindUserWithRolesByIdAsync(int UserId)
        //{
        //    return await Users.Where(u => u.Id == UserId).Select(user => new UsersViewModel
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        UserName = user.UserName,
        //        PhoneNumber = user.PhoneNumber,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        BirthDate = user.BirthDate,
        //        IsActive = user.IsActive,
        //        Image = user.Image,
        //        RegisterDateTime = user.RegisterDateTime,
        //       // Roles = user.Roles,
        //        RoleName = user.Roles.First().Role.Name,
        //        Bio=user.Bio,
        //        AccessFailedCount = user.AccessFailedCount,
        //        EmailConfirmed = user.EmailConfirmed,
        //        LockoutEnabled = user.LockoutEnabled,
        //        LockoutEnd = user.LockoutEnd,
        //        PhoneNumberConfirmed = user.PhoneNumberConfirmed,
        //        TwoFactorEnabled = user.TwoFactorEnabled,
        //        Gender = user.Gender,
        //    }).FirstOrDefaultAsync();
        //}

        //public async Task<string> GetFullName(ClaimsPrincipal User)
        //{
        //    var UserInfo = await GetUserAsync(User);
        //    return UserInfo.FirstName + " " + UserInfo.LastName;
        //}

        //public async Task<List<UsersViewModel>> GetPaginateUsersAsync(int offset, int limit, string orderBy, string searchText)
        //{
        //   var getDateTimesForSearch = searchText.GetDateTimeForSearch();
        //   var users = await Users.Include(u => u.Roles)
        //         .Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText) || t.Email.Contains(searchText) || t.UserName.Contains(searchText) || (t.RegisterDateTime >= getDateTimesForSearch.First() && t.RegisterDateTime <= getDateTimesForSearch.Last()))
        //         .OrderBy(orderBy)
        //         .Skip(offset).Take(limit)
        //         .Select(user => new UsersViewModel
        //         {
        //            Id = user.Id,
        //            Email = user.Email,
        //            UserName = user.UserName,
        //            PhoneNumber = user.PhoneNumber,
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //            IsActive = user.IsActive,
        //            Image = user.Image,
        //            Bio = user.Bio,
        //            PersianBirthDate = user.BirthDate.ConvertMiladiToShamsi("yyyy/MM/dd"),
        //            PersianRegisterDateTime = user.RegisterDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm:ss"),
        //            GenderName = user.Gender == GenderType.Male ? "مرد" : "زن",
        //            RoleId = user.Roles.Select(r => r.Role.Id).FirstOrDefault(),
        //            RoleName = user.Roles.Select(r => r.Role.Name).FirstOrDefault()
        //         }).AsNoTracking().ToListAsync();

        //   foreach (var item in users)
        //      item.Row = ++offset;

        //   return users;
        //}

        //public async Task<List<UsersViewModel>> GetPaginateUsersAsync(int offset, int limit, bool? firstnameSortAsc, bool? lastnameSortAsc, bool? emailSortAsc, bool? usernameSortAsc,bool? registerDateTimeSortAsc, string searchText)
        //{
        //    var users = await Users.Include(u => u.Roles).Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText) || t.Email.Contains(searchText) || t.UserName.Contains(searchText) || t.RegisterDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm:ss").Contains(searchText))
        //            .Select(user => new UsersViewModel
        //            {
        //                Id = user.Id,
        //                Email = user.Email,
        //                UserName = user.UserName,
        //                PhoneNumber = user.PhoneNumber,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                IsActive = user.IsActive,
        //                Image = user.Image,
        //                Bio=user.Bio,
        //                PersianBirthDate = user.BirthDate.ConvertMiladiToShamsi("yyyy/MM/dd"),
        //                PersianRegisterDateTime = user.RegisterDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm:ss"),
        //                GenderName = user.Gender == GenderType.Male ? "مرد" : "زن",
        //                RoleId = user.Roles.Select(r => r.Role.Id).FirstOrDefault(),
        //                RoleName = user.Roles.Select(r => r.Role.Name).FirstOrDefault()
        //            }).Skip(offset).Take(limit).ToListAsync();

        //    if (firstnameSortAsc != null)
        //    {
        //        users = users.OrderBy(t => (firstnameSortAsc == true && firstnameSortAsc != null) ? t.FirstName : "").OrderByDescending(t => (firstnameSortAsc == false && firstnameSortAsc != null) ? t.FirstName : "").ToList();
        //    }

        //    else if (lastnameSortAsc != null)
        //    {
        //        users = users.OrderBy(t => (lastnameSortAsc == true && lastnameSortAsc != null) ? t.LastName : "").OrderByDescending(t => (lastnameSortAsc == false && lastnameSortAsc != null) ? t.LastName : "").ToList();
        //    }

        //    else if (emailSortAsc != null)
        //    {
        //        users = users.OrderBy(t => (emailSortAsc == true && emailSortAsc != null) ? t.Email : "").OrderByDescending(t => (emailSortAsc == false && emailSortAsc != null) ? t.Email : "").ToList();
        //    }

        //    else if (usernameSortAsc != null)
        //    {
        //        users = users.OrderBy(t => (usernameSortAsc == true && usernameSortAsc != null) ? t.PhoneNumber : "").OrderByDescending(t => (usernameSortAsc == false && usernameSortAsc != null) ? t.UserName : "").ToList();
        //    }

        //    else if (registerDateTimeSortAsc != null)
        //    {
        //        users = users.OrderBy(t => (registerDateTimeSortAsc == true && registerDateTimeSortAsc != null) ? t.PersianRegisterDateTime : "").OrderByDescending(t => (registerDateTimeSortAsc == false && registerDateTimeSortAsc != null) ? t.PersianRegisterDateTime : "").ToList();
        //    }

        //    foreach (var item in users)
        //        item.Row = ++offset;

        //    return users;
        //}

        //public string CheckAvatarFileName(string fileName)
        //  {
        //      string fileExtension = Path.GetExtension(fileName);
        //      int fileNameCount = Users.Where(f => f.Image == fileName).Count();
        //      int j = 1;
        //      while (fileNameCount != 0)
        //      {
        //          fileName = fileName.Replace(fileExtension, "") + j + fileExtension;
        //          fileNameCount = Users.Where(f => f.Image == fileName).Count();
        //          j++;
        //      }

        //      return fileName;
        //  }

        public Task<UserEntity> FindClaimsInUser(Guid userId)
        {
            return Users.Include(c => c.Claims).FirstOrDefaultAsync(c => c.Id == userId);
        }


        public async Task<IdentityResult> AddOrUpdateClaimsAsync(Guid userId, string userClaimType, IList<string> selectedUserClaimValues)
        {
            var user = await FindClaimsInUser(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "NotFound",
                    Description = "کاربر مورد نظر یافت نشد.",
                });
            }

            var CurrentUserClaimValues = user.Claims.Where(r => r.ClaimType == userClaimType).Select(r => r.ClaimValue).ToList();
            if (selectedUserClaimValues == null)
                selectedUserClaimValues = new List<string>();

            var newClaimValuesToAdd = selectedUserClaimValues.Except(CurrentUserClaimValues).ToList();
            foreach (var claim in newClaimValuesToAdd)
            {
                user.Claims.Add(new UserClaimEntity
                {
                    UserId = userId,
                    ClaimType = userClaimType,
                    ClaimValue = claim,
                });
            }

            var removedClaimValues = CurrentUserClaimValues.Except(selectedUserClaimValues).ToList();
            foreach (var claim in removedClaimValues)
            {
                var roleClaim = user.Claims.SingleOrDefault(r => r.ClaimValue == claim && r.ClaimType == userClaimType);
                if (roleClaim != null)
                    user.Claims.Remove(roleClaim);
            }

            return await UpdateAsync(user);
        }

    }
}
