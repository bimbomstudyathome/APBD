namespace LegacyApp
{
    public class UserValidator : IUserValidator
    {
        public bool Validate(User user)
        {
            return string.IsNullOrEmpty(user.FirstName) && string.IsNullOrEmpty(user.LastName);
        }
    }
}