using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersTeamsController : ControllerBase
    {
        private readonly AppDbContext db;

        public UsersTeamsController(AppDbContext DB)
        {
            db = DB;
        }

        [HttpGet("exists/{userId}/{teamId}")]
        public async Task<bool> DoUsersExist(Guid userId, Guid teamId)
        {
            return await db.userteams.AnyAsync(x => x.UserId == userId && x.TeamId == teamId);
        }

        [HttpGet("get-teams/user/{userId}")]
        public async Task<List<Team>> GetTeamsByUser(Guid userId)
        {
            var userteams = await db.userteams
                .Where(user => user.UserId == userId)
                .ToListAsync();
            List<Team> teams = new List<Team>();
            foreach(UsersTeams entry in userteams)
            {
                var foundTeam = await db.teams.FindAsync(entry.TeamId);
                teams.Add(foundTeam!);
            }
            return teams;
        }

        [HttpGet("get-users/team/{teamId}")]
        public async Task<List<User>> GetUsersByTeam(Guid teamId)
        {
            var userteams = await db.userteams
                .Where(team => team.TeamId == teamId)
                .ToListAsync();
            List<User> users = new();
            foreach (UsersTeams entry in userteams)
            {
                var foundUser = await db.users.FindAsync(entry.UserId);
                users.Add(foundUser!);
            }
            return users;
        }

        [HttpPost("add-userteams")]
        public async Task<UsersTeams> AddUserTeams([FromBody] UsersTeams userTeams)
        {
            var teams = await db.userteams
                .AddAsync(userTeams);
            await db.SaveChangesAsync();
            return teams.Entity;
        }


    }
}

