using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Infrastructure.Authentication;

/// <summary>
/// Password hashing service using BCrypt
/// </summary>
public class PasswordHasher
{
    /// <summary>
    /// Hash a password
    /// </summary>
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Verify a password against a hash
    /// </summary>
    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}