using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trello_clone.Shared.Classes;

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

        [HttpGet("exist")]
        public async Task<bool> DoUsersExist()
        {
            return await db.users.AnyAsync();
        }

        [HttpGet("{id}")]
        public async Task<User?> GetById(Guid id) => await db.users.FindAsync(id);

        [HttpGet("get-users")]
        public async Task<List<User>> GetUsers()
        {
            return await db.users.ToListAsync();
        }

        [HttpPost("add-user")]
        public async Task<User> Post([FromBody] User user)
        {
            if (db.users.Any(u => u.UserName == user.UserName)) //checking if a username already exists
            {
                return null!;
            }
            else
            {
                await db.users.AddAsync(user);
                await db.SaveChangesAsync();
                return user;
            }
        }

        [HttpPut("update/{id}")]
        public async Task<User?> Put([FromBody] User updatedUser, Guid id)
        { 
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
