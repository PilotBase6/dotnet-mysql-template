using Infrastructure.Core;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Entities
{
    public class User : IUser
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User(string name, string email, string password)
        {
            UserId = Guid.NewGuid();
            Name = ValidateName(name);
            Email = ValidateEmail(email);
            SetPassword(password);
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.MinValue;
        }

        public static string ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The name cannot be empty.");
            }
            if (name.Length < 3)
            {
                throw new ArgumentException("The name must be at least 3 characters long.");
            }
            return name;
        }

        public static string ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("The email cannot be empty.");
            }
            if (!email.Contains("@"))
            {
                throw new ArgumentException("The email must contain an @ symbol.");
            }
            if (!email.Contains("."))
            {
                throw new ArgumentException("The email must contain a dot.");
            }
            if (email.Length < 5)
            {
                throw new ArgumentException("The email must be at least 5 characters long.");
            }
            return email;
        }

        public static string ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("The password cannot be empty.");
            }
            if (password.Length < 8)
            {
                throw new ArgumentException("The password must be at least 8 characters long.");
            }
            if (!password.Any(char.IsUpper))
            {
                throw new ArgumentException("The password must contain at least one uppercase letter.");
            }
            if (!password.Any(char.IsLower))
            {
                throw new ArgumentException("The password must contain at least one lowercase letter.");
            }
            if (!password.Any(char.IsDigit))
            {
                throw new ArgumentException("The password must contain at least one number.");
            }
            if (password.Any(char.IsWhiteSpace))
            {
                throw new ArgumentException("The password cannot contain whitespace.");
            }
            if (!password.Any(char.IsPunctuation))
            {
                throw new ArgumentException("The password must contain at least one special character.");
            }
            return password;
        }

        public void SetPassword(string password)
        {
            Password = HashPassword(password);
            UpdatedAt = DateTime.Now;
        }

        private static string HashPassword(string password)
        {
            var saltGuid = "1234567890qwertyuiopasdfghjklzxcvbnm";
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Concat(password, saltGuid);
                var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                var hashBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(string password)
        {
            var hash = HashPassword(password);
            var reHash = HashPassword(hash);
            return reHash == Password;
        }
    }
}