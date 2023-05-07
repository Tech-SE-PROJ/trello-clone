using Microsoft.EntityFrameworkCore;
using trello_clone.Server.Controllers;
using trello_clone.Shared.Classes;

namespace trello_clone.Server
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Team> teams { get; set; }
        public DbSet<TaskCard> board_cards { get; set; }
        public DbSet<UsersTeams> userteams { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}