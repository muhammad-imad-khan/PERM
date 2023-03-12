namespace Perm.Security.AuthenticateManager
{
    public static class HashPassword
    {
        public static string Hash(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword, 10);
        }

        public static bool Verify(string plainPassword, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashed);
        }
    }
}