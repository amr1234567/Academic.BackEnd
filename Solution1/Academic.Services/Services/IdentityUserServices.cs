
using System.Security.Claims;

namespace Academic.Services.Services
{
    public class IdentityUserServices
        (ITokenServices tokenServices, IUnitOfWork unitOfWork,
        IUserIdentityRepository userIdentityRepository, IMapper mapper,
        AccountServicesHelpers accountServices)
        : IIdentityUserServices
    {
        public async Task<UserDto> GetUserData(int userId)
        {
            var user = await userIdentityRepository.GetById(userId);
            if (user == null)
                throw new EntityNotFoundException(typeof(IdentityUser), userId);
            return mapper.Map<UserDto>(user);
        }

        public async Task<TokenModel> SignIn(string email, string password)
        {
            var user = await userIdentityRepository.GetByEmail(email);
            if (user == null)
                throw new BadRequestExecption("email or password wrong");

            var checkPasswordResult = CheckPasswordWithHashed(user.Password, password, user.Salt);
            if (!checkPasswordResult)
                throw new BadRequestExecption("email or password wrong");
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Role, user.Role.ToString()),
                new(ClaimTypes.Sid , Guid.NewGuid().ToString())
            };
            var token = await tokenServices.GenerateNewTokenModel(user.Id, claims);
            await unitOfWork.SaveChangesAsync();
            return token;
        }

        public async Task<int> SignOut(string email)
        {
            var user = await userIdentityRepository.GetByEmail(email);
            if (user == null)
                throw new BadRequestExecption("email is wrong");
            await tokenServices.RevokeTokenWithUserId(user.Id);
            await unitOfWork.SaveChangesAsync();
            return  1;
        }

        private bool CheckPasswordWithHashed(string hashedPassword, string givenPassword, string salt)
        {
            var givenPasswordHashed = accountServices.HashPasswordWithSalt(salt, givenPassword);
            return hashedPassword == givenPasswordHashed;
        }
    }
}
