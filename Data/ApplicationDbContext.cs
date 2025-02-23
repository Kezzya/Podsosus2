using Microsoft.EntityFrameworkCore;
using Podsosus2.Models;

namespace Podsosus2.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Конструктор контекста
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet для игроков
        public DbSet<Player> Players { get; set; }

        // DbSet для клеток
        public DbSet<Cell> Cells { get; set; }
    }
}