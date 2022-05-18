namespace Yella.Core.IdentityService.Helpers.Security.Hashing;

public interface IPasswordHasher
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}