using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace LoginAndRegistration.Database
{
    public class PasswordHasher
    {
        // Hash the password by using BCrypt library, which also have Salt in it
        // (Salt is an extra important layer that can add another protection layer)
        public string HashPassword(string password)
        {
        // Use bcrypt to hash the password; it automatically includes a salt
        return BCrypt.Net.BCrypt.HashPassword(password);
        }
        
        // Verify a password
    public bool VerifyPassword(string password, string hashedPassword)
    {
        // Use bcrypt to verify the password against the hashed password
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
    }
}
