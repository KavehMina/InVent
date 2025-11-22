using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Data
{
    public class EntityDBContext(DbContextOptions<EntityDBContext> options) : DbContext(options)
    {
        public DbSet<Tanker> Tankers { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Refinery> Refineries { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<Customs> Customs { get; set; }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public DbSet<Project> Projects { get; set; }             
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Dispatch> Dispatches { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

    }
}
