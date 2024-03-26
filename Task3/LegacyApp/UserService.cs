using System;
using System.Collections;
using System.Collections.Generic;
namespace LegacyApp
{
    public class UserService
    {
        private readonly IEnumerable<IUserValidator> _userValidators;
        private readonly ClientValidator _clientValidator;

        public UserService()
        {
            _clientValidator = new ClientValidator();
            _userValidators = new List<IUserValidator>{new UserValidator(), new EmailValidator(), new UserAgeValidator()};
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            foreach (var validator in _userValidators)
            {
                validator.Validate(user);
            }

            _clientValidator.Validate(user, client);

            UserDataAccess.AddUser(user);
            return true;
        }
        
    }
    
}
