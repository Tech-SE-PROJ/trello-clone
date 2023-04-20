//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using trello_clone.Shared.Classes;
//using trello_clone.Server.Interfaces;
//using Microsoft.Data.SqlClient;
//using trello_clone.Shared;

//namespace trello_clone.Server.Services
//{
//    public class LoginService : ILoginService
//    {
//        private readonly AppDbContext _dbContext;
//        //private SqlConnection _con = new(LocalServerConnection.ConnectionString);
//        private StateContainer stateContainer;
//        //public void Register(string userName, string userPass, string userEmail)
//        //{
//        //    _con.Open();
//        //    using (SqlTransaction trans = _con.BeginTransaction())
//        //    {
//        //        using (SqlCommand cmd = _con.CreateCommand())
//        //        {
//        //            cmd.CommandText = @"INSERT INTO users (userId,firstName,lastName,jobTitle,userName,userPassword,email,teamId) VALUES (@userId,'','','',@userName,@userPass,@userEmail,'')";
                    
//        //            cmd.Parameters.AddWithValue("@userId", Guid.NewGuid());
//        //            cmd.Parameters.AddWithValue("@userName", userName);
//        //            cmd.Parameters.AddWithValue("@userPass", userPass);
//        //            cmd.Parameters.AddWithValue("@userEmail", userEmail);

//        //            cmd.ExecuteNonQuery();
//        //            trans.Commit();
//        //        }
//        //    }
//        //    _con.Close();
//        //}
        
//        //public void Login(string userName, string userPass)
//        //{
            
//        //    var user = new User();

//        //    string query = "SELECT * FROM users WHERE userName = @userName AND userPassword = @userPass";
//        //    using SqlCommand cmd = new(query, _con);
//        //    _con.Open();
//        //    cmd.Parameters.AddWithValue("@userName", userName);
//        //    cmd.Parameters.AddWithValue("@userPass", userPass);
//        //    SqlDataReader dr = cmd.ExecuteReader();
//        //    while (dr.Read())
//        //    {
//        //        try
//        //        {
//        //            user.Username = (string)dr["userName"];
//        //            user.Password = (string)dr["userPass"];
                    
//        //            stateContainer.LoggedInUser = user;
//        //        }
//        //        catch (Exception)
//        //        {
//        //            Console.Write("Failure to login");
//        //        }
//        //    }
//        //    _con.Close();
//        //}
//        public LoginService(AppDbContext dbContext)
//        {
//            _dbContext = dbContext;
//            stateContainer = new StateContainer();
//        }

//        public void RegisterUser(User user)
//        {
//            _dbContext.users.Add(user);
//            _dbContext.SaveChanges();
//            //if (user.Id.)
//            //{
//            //    _dbContext.User.Add(user)
//            //}
//        }

//        public List<User> GetUsers()
//        {
//            return _dbContext.users.ToList();
//        }

//        //public User GetUserById(Guid id)
//        //{
//        //    var user = _dbContext.users.Find(id);
//        //    Console.WriteLine($"...found user {user.userName}");
//        //    return user;
//        //}

//        //public User GetUserByName(string username, string pass)
//        //{
//        //    var user = _dbContext.users.Find(username);
//        //    Console.WriteLine($"...found user with {user.userName} and pass: {user.userPassword}");
//        //    if (user.userPassword == pass)
//        //    {
//        //        Console.WriteLine($"...found user with {user.userName} and pass: {user.userPassword}");
//        //    }
//        //    else { Console.WriteLine("Incorrect password."); }
//        //    return user;
//        //}
//    }
//}