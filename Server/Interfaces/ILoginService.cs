using trello_clone.Shared.Classes;

namespace trello_clone.Server.Interfaces
{
    public interface ILoginService
    {
        //public void Register(string userName, string userPass, string userEmail);
        //public void Login(string userName, string userPass);
        void RegisterUser(User user);
        List<User> GetUsers();
        User GetUserById(Guid id);
        User GetUserByName(string username, string pass);
    }
}