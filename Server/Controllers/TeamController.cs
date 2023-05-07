using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly AppDbContext db;

        public TeamController(AppDbContext DB)
        {
            db = DB;
        }

        [HttpGet("{id}")]
        public async Task<Team?> GetTeamById(Guid id) => await db.teams.FindAsync(id);


        [HttpGet("get-teams")]
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await db.teams.ToListAsync();
        }

        [HttpGet("name/{teamName}")]
        public async Task<Team?> GetTeamByName(string? teamName)
        {
            return await db.teams.FirstOrDefaultAsync(x => x.TeamName == teamName);
        }

        [HttpPost("add-team")]
        public async Task Post([FromBody] Team team)
        {
            if (db.teams.Any(t => t.TeamName == team.TeamName)) //checking if a username already exists
            {
                return;
            }
            await db.teams.AddAsync(team);
            await db.SaveChangesAsync();
        }

        //[HttpGet("get-teams/user/{userId}")]
        //public async Task<List<Team>?> GetTeamsForUser(Guid userId)
        //{
        //    return db.users
        //        .Where(user => user.userId == userId)
        //        .Include(team => team.Teams)
        //        .SelectMany(user => user.Teams)
        //        .ToList();
        //    //var teams = await db.teams //for each team, show that and only that for the user and any they create, but not all in the database
        //    //    .Where(team => team.Users.FirstOrDefault().UserId == userId)
        //    //    .Include(team => team.TeamName)
        //    //    //.SelectMany(user => user.Teams)
        //    //    .ToListAsync();
        //    //return teams;
        //    //var abc =  db.users.Where(u => u.UserId == userId).SelectMany(c => c.Teams);
        //    //return abc;
        //}
    }
}