using System;

namespace LegacyApp
{
    public class UserAgeValidator : IUserValidator
    {
        public bool Validate(User user)
        {
            var now = DateTime.Now;
            var age = now.Year - user.DateOfBirth.Year;
            if (now.Month < user.DateOfBirth.Month ||
                (now.Month == user.DateOfBirth.Month && now.Day < user.DateOfBirth.Day)) age--;
            return age >= 21;
        }
    }
}