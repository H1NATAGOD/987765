namespace AvaloniaApplication2.Services;

public class Auth
{
    public enum UserRole
    {
        User,
        Employee,
        Admin
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }
}