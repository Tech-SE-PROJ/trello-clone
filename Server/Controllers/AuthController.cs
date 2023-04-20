using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using trello_clone.Shared.Classes;
using static MudBlazor.Icons;

namespace trello_clone.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext db;

        public AuthController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public List<User> Get()
        {
            return db.users.ToList();
        }

        [HttpPost]
        public async Task<User?> Post([FromBody] User user)
        {
            if (db.users.Any(u => u.UserName == user.UserName)) //checking if a username already exists
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
        public Task<User?>? Login([FromBody] User user)
        {
            if (db.users.Any(u => !(u.UserName == user.UserName)) || 
                db.users.Any(u => !(u.UserPassword == user.UserPassword))) //checking if a username doesn't exist
            {
                return null;
            }
            else
            {
                using (db)
                {
                    User? foundUser = db.users
                                    .Where(b => b.UserName == $"{user.UserName}")
                                    .FirstOrDefault();
                    return Task.FromResult(foundUser); //test
                }
            }
        }

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
    }
}
