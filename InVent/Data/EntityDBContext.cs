using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Data
{
    public class EntityDBContext(DbContextOptions<EntityDBContext> options) : DbContext(options)
    {
        public DbSet<Tanker> Tankers { get; set; }
        public DbSet<Bank> Banks { get; set; }
    }
}
