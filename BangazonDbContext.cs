using Microsoft.EntityFrameworkCore;
using Bangazon.Models;
using System.Runtime.CompilerServices;

public class BangazonDbContext : DbContext
{

    public DbSet<Category> Categories { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PaymentTypes> PaymentTypes { get; set; }
    public DbSet<OrderProducts> OrderProducts { get; set; }




    public BangazonDbContext(DbContextOptions<BangazonDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category {
                ID = 1,
                Name = "Home Decore"
            },

            new Category {
                ID = 2,
                Name = "Sport Goods"
            },
        });

       
    }
}