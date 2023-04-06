using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trello_clone.Shared.Classes;
using trello_clone.Server.Interfaces;
using trello_clone.Server;
using Microsoft.Data.SqlClient;

namespace trello_clone.Server.Services
{
    public class LoginService : ILoginService
    {
        private SqlConnection _con = new SqlConnection(LocalServerConnection.ConnectionString);
        private ILoginService _loginService = new LoginService();

        public void UserRegistration(string userName, string userPass, string userEmail)
        {
            _con.Open();
            using (SqlTransaction trans = _con.BeginTransaction())
            {
                using (SqlCommand cmd = _con.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO users (userId,firstName,lastName,jobTitle,userName,userPassword,email,teamId) VALUES (@userId,'','','',@userName,@userPass,@userEmail,'')";
                    
                    cmd.Parameters.AddWithValue("@userId", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Parameters.AddWithValue("@userPass", userPass);
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);

                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
            }
            _con.Close();
        }
        
        public void GetUserAccount(string userName, string userPass)
        {
            var user = new User();

            string query = "SELECT * FROM users WHERE userName = @userName AND userPassword = @userPass";
            using SqlCommand cmd = new(query, _con);
            _con.Open();
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@userPass", userPass);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    user.Username = (string)dr["userName"];
                    user.Password = (string)dr["userPass"];
                    
                }
                catch (Exception)
                {
                    Console.Write("Failure to login");
                }
            }
            _con.Close();
        }
        public LoginService()
        {
            
        }
    }
}