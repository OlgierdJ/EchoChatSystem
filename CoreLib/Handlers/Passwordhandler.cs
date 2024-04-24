using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Handlers
{
    public class Passwordhandler : IPasswordHandler
    {
        //
        //https://code-maze.com/csharp-hashing-salting-passwords-best-practices/
        private readonly ISecurityCredentialsRepository _PasswordRepo;
        public Passwordhandler(ISecurityCredentialsRepository PasswordRepo)
        {
            _PasswordRepo = PasswordRepo;
        }
        public Passwordhandler()
        {

        }
        public async Task<bool> CheckPassword(string Password, ulong UserId)
        {
            var creds = await _PasswordRepo.GetAllAsync();
            var UserPassword = creds.First(obj => obj.UserId == UserId);

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
                return credentials;
            }

        }

    }
}
