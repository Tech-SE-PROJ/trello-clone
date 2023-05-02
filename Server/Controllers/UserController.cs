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
        public List<User> GetUsers()
        {
            return db.users.ToList();
        }

        [HttpGet("get-users/{teamId}")]
        public IEnumerable<User> GetUsersByTeam(int teamId)
        {
            return db.users.ToList().Where(user => user.TeamId == teamId);
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

        [HttpGet("{id}")]
        public async Task<User?> GetById(Guid id) => await db.users.FindAsync(id);

        [HttpPut("update/{id}")]
        public async Task<User?> Put(Guid id, [FromBody] User updatedUser)
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
