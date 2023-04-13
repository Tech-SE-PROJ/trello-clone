using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using trello_clone.Server.Interfaces;
using trello_clone.Server.Services;
using trello_clone.Shared;
using trello_clone.Shared.Classes;
using static MudBlazor.Icons;

namespace trello_clone.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext db;
        private ILoginService _loginService;

        public AuthController(AppDbContext db)
        {
            this.db = db;
            _loginService = new LoginService(db);
        }

        [HttpGet]
        public List<User> Get()
        {
            return db.users.ToList();
        }

        [HttpPost]
        public async Task<User> Post([FromBody] User user)
        {
            //var u = db.users.FirstOrDefault(x => x.userName == user.userName);
            if (db.users.Any(u => u.userName == user.userName)) //checking if a username already exists
            {
                return null;
            }
            else
            {
                EntityEntry<User> entry = await db.users.AddAsync(user);
                await db.SaveChangesAsync();
                return entry.Entity;
            }
        }

        [HttpGet("Login")]
        public async Task<User> Login([FromBody] User user)
        {
            if (db.users.Any(u => !(u.userName == user.userName)) || 
                db.users.Any(u => !(u.userPassword == user.userPassword))) //checking if a username doesn't exist
            {
                return null;
            }
            else
            {
                using (db)
                {
                    User? foundUser = db.users
                                    .Where(b => b.userName == $"{user.userName}")
                                    .FirstOrDefault();
                    return foundUser;
                }
                //EntityEntry<User> entry = await db.users.FindAsync();
                ////var foundUser = db.users.Where(u => u.userName. == user.userName);
                //return _context.HttpContext.User.Identity.Name;
                //StateContainer.LoggedInUser = user;
                //return 
            }
        }
        //private bool Search(User user)
        //{
        //    var foundUser = db.users.Any(u => u.userName == user.userName);
        //    if (user.userName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
        //        || customer.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
        //        || customer.PhoneNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //[HttpGet("{id}")]
        //public async Task<User> GetById(Guid id)
        //{
        //    return await db.users.FindAsync(id);
        //}

        //[HttpPut("{id}")]
        //public async Task<User> Put(Guid id, [FromBody] User user)
        //{
        //    User? edit = await db.User.FindAsync(id);
        //    if(edit != null)
        //    {
        //        edit.Username = user.Username;
        //        edit.Password = user.Password;
        //        edit.Email = user.Email;
        //        await db.SaveChangesAsync();
        //    }
        //    return edit;
        //}

        //[HttpDelete("{id}")]
        //public async Task<User> Delete(Guid id)
        //{
        //    User? user = await db.User.FindAsync(id);
        //    if (user != null)
        //    {
        //        db.User.Remove(user);
        //        await db.SaveChangesAsync();
        //    }
        //    return user;
        //}

        //[HttpGet]
        //public async Task<IEnumerable<User>> Get(string name)
        //{
        //    return await Task.Factory.StartNew<IEnumerable<User>>(() =>
        //    {
        //        if (string.IsNullOrEmpty(name))
        //            return db.User;
        //        else
        //            return db.User.Where(user => user.Username.Contains(name));
        //    });
        //}

        //[HttpGet("Register/{userName}/{userPass}/{userEmail}")]
        //public void Register(string userName, string userPass, string userEmail)
        //{
        //    _loginService.Register(userName, userPass, userEmail);
        //}

        //[HttpGet("Login/{userName}/{userPass}")]
        //public void Login(string userName, string userPass)
        //{
        //    _loginService.GetUserAccount(userName, userPass);
        //}
    }
}
