using Microsoft.EntityFrameworkCore;
using static MudBlazor.Icons;

namespace trello_clone.Server
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Shared.Classes.User> users { get; set; }
        public DbSet<Shared.Classes.TaskCard> board_cards { get; set; }
    }
}
