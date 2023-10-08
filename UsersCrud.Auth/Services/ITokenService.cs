namespace UsersCrud.Auth.Services
{
    public interface ITokenService
    {
        string CreateToken(string email);
    }
}
