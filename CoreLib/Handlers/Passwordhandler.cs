using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using System.Security.Cryptography;

namespace CoreLib.Handlers
{
    public class Passwordhandler : IPasswordHandler
    {
        //https://code-maze.com/csharp-hashing-salting-passwords-best-practices/
        private readonly ISecurityCredentialsRepository _PasswordRepo;
        public Passwordhandler(ISecurityCredentialsRepository PasswordRepo)
        {
            _PasswordRepo = PasswordRepo;
        }
        public Passwordhandler(ISecurityCredentialsService security)
        {

        }
        public async Task<bool> CheckPassword(string Password, ulong UserId)
        {
            var UserPassword = await _PasswordRepo.GetSingleAsync(obj => obj.UserId == UserId);

            using (var pbkdf2 = new Rfc2898DeriveBytes(Password, UserPassword.Salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                return hash.SequenceEqual(UserPassword.PasswordHash);
            }

        }

        public async Task<SecurityCredentials> CreatePassword(string password, ulong Userid)
        {

            // Generate a random salt value.
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Use the PBKDF2 algorithm to generate a hash and salt for the password.
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                SecurityCredentials credentials = new()
                {
                    PasswordHash = hash,
                    Salt = salt,
                    UserId = Userid
                };
                await _PasswordRepo.AddAsync(credentials);
                return credentials;
            }

        }

        public async Task<SecurityCredentials> UpdatePassword(string password, ulong Userid)
        {
            var UserPassword = await _PasswordRepo.GetSingleAsync(obj => obj.UserId == Userid);

            // Generate a random salt value.
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Use the PBKDF2 algorithm to generate a hash and salt for the password.
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                SecurityCredentials credentials = new()
                {
                    PasswordHash = hash,
                    Salt = salt,
                    UserId = Userid,
                    Id = UserPassword.Id
                };
                await _PasswordRepo.UpdateAsync(credentials);
                return credentials;
            }

        }

    }
}
