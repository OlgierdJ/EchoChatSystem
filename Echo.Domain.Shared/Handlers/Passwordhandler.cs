﻿using Echo.Domain.Shared.Entities.EchoCore.UserCore;
using Echo.Domain.Shared.Interfaces.Handlers;
using Echo.Domain.Shared.Interfaces.Repositories;
using System.Security.Cryptography;

namespace Echo.Domain.Shared.Handlers;

public class Passwordhandler : IPasswordHandler
{
    //https://code-maze.com/csharp-hashing-salting-passwords-best-practices/
    
    public async Task<bool> CheckPassword(string Password, SecurityCredentials userPwd)
    {
        //var UserPassword = await _PasswordRepo.GetSingleAsync(obj => obj.UserId == UserId);

        using (var pbkdf2 = new Rfc2898DeriveBytes(Password, userPwd.Salt, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(20);

            return hash.SequenceEqual(userPwd.PasswordHash);
        }

    }

    public async Task<SecurityCredentials> CreatePassword(string password)
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
            };
            //await _PasswordRepo.AddAsync(credentials);
            return credentials;
        }

    }
}