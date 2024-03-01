using Microsoft.EntityFrameworkCore;
using Bangazon.Models;

public class BangazonDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    public BangazonDbContext(DbContextOptions<BangazonDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new()
            {
                ID = 1,
                Name = "Home Decore"
            },
            new()
            {
                ID = 2,
                Name = "Sport Goods"
            }
        });

        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new()
            {
                ID = 1,
                UserId = 1,
                OrderIsOpen = false,
                OrderDate = new DateTime(2024, 1, 6),
                PaymentType = 1
            },
            new()
            {
                ID = 2,
                UserId = 2,
                OrderIsOpen = true,
                OrderDate = new DateTime(2024, 1, 24),
                PaymentType = 2
            }
        });

        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new()
            {
                ID = 1,
                SellerId = 1,
                ProductName = "Movement Sensor Lamp",
                CategoryId = 1,
                QuantityInStock = 2,
                Description = "This item is a lamp that has no buttons to turn on, but the bottom rim of the lamp shade has a movement sensor to detect when someone or something walks by, thus turning it on.",
                PricePerUnit = 25.00M,
                TimePosted = new DateTime(2024, 2, 16)
            },
            new()
            {
                ID = 2,
                SellerId = 2,
                ProductName = "Aluminum bat",
                CategoryId = 2,
                QuantityInStock = 5,
                Description = "A baseball bat made out of aluminum.",
                PricePerUnit = 40.00M,
                TimePosted = new DateTime(2024, 2, 4)
            }
        });

        modelBuilder.Entity<User>().HasData(new User[]
        {
            new() 
            {
                ID = 1,
                Email = "Jjonna@dailybugle.com",
                Name = "John Jonah Jameson Jr",
                IsSeller = true
            },
            new()
            {
                ID = 2,
                Email = "AuntieMayLovesBen@gmail.com",
                Name = "Maybelle Parker-Jameson",
                IsSeller = false
            },
            new() 
            {
                ID = 3,
                Email = "PeterParker@ESU.com",
                Name = "Peter Parker",
                IsSeller = true
            }
        });

        modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
        {
            new()
            {
                ID = 1,
                Name = "Debit"
            },
            new()
            { 
                ID = 2,
                Name = "Credit"
            }
        });

        modelBuilder.Entity<OrderProduct>().HasData(new OrderProduct[]
        {
            new()
            { 
                ID = 1,
                OrderId = 1,
                ProductId = 1
            },
            new()
            {
                ID = 2,
                OrderId = 2,
                ProductId = 2
            }
        });
    }
}
