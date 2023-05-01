using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using trello_clone.Shared.Classes;
using static MudBlazor.Icons;

namespace trello_clone.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext db;

        public UserController(AppDbContext DB)
        {
            db = DB;
        }

        [HttpGet("get-users")]
        public List<User> Get()
        {
            return db.users.ToList();
        }

        [HttpPost("add-user")]
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

        //[HttpGet("login")]
        //public Task<User?>? Login([FromBody] User user)
        //{
        //    if (db.users.Any(u => !(u.UserName == user.UserName)) ||
        //        db.users.Any(u => !(u.UserPassword == user.UserPassword))) //checking if a username/password doesn't exist or match
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        using (db)
        //        {
        //            User? foundUser = db.users
        //                            .Where(b => b.UserName == $"{user.UserName}")
        //                            .FirstOrDefault();
        //            return Task.FromResult(foundUser);
        //        }
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<User?> GetById(Guid id) => await db.users.FindAsync(id); //fix this

        [HttpPut("update/{id}")]
        public async Task<User?> Put(Guid id, [FromBody] User updatedUser) //to change user fields, create a new GUID and send it here
        { //tf does this do
            User? oldUserUpdated = await db.users.FindAsync(id);
            if (oldUserUpdated != null)
            {
                oldUserUpdated = updatedUser;
                await db.SaveChangesAsync();
            }
            return oldUserUpdated;
        }

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
