namespace trello_clone.Server.Interfaces
{
    public interface ILoginService
    {
        public void UserRegistration(string userName, string userPass, string userEmail);
        public void GetUserAccount(string userName, string userPass);
    }
}