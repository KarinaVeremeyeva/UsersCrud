using Microsoft.AspNetCore.Identity;

namespace UsersCrud.Auth.Services
{
    public interface IAccountService
    {
        Task<SignInResult> LoginAsync(string email, string password);

        Task LogoutAsync();
    }
}
