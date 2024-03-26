namespace LegacyApp
{
    public class EmailValidator : IUserValidator
    {
        public bool Validate(User user)
        {
            return user.EmailAddress.Contains("@") || user.EmailAddress.Contains(".");
        }
    }
}