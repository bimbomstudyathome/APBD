namespace LegacyApp
{
    public interface IUserCreditService
    {
        public bool Validate(User user, Client client);
    }
}