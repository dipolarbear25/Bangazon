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
            new Category
            {
                ID = 1,
                Name = "Home Decore"
            },
            new Category
            {
                ID = 2,
                Name = "Sport Goods"
            }
        });

        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order
            {
                ID = 1,
                UserId = 1,
                OrderIsOpen = false,
                OrderDate = new DateTime(2024, 1, 6),
                PaymentType = 1
            }
        });

        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product
            {
                ID = 1,
                SellerId = 1,
                ProductName = "Movement Sensor Lamp",
                CategoryId = 1,
                QuantityInStock = 2,
                Description = "This item is a lamp that has no buttons to turn on, but the bottom rim of the lamp shade has a movement sensor to detect when someone or something walks by, thus turning it on.",
                PricePerUnit = 25.00M,
                TimePosted = new DateTime(2024, 2, 16)
            }
        });

        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User
            {
                ID = 1,
                Email = "Jjonna@dailybugle.com",
                Name = "John Jonah Jameson Jr",
                IsSeller = true
            }
        });

        modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
        {
            new PaymentType
            {
                ID = 1,
                Name = "Debit"
            },
            new PaymentType
            { 
                ID = 2,
                Name = "Credit"
            }
        });

        modelBuilder.Entity<OrderProduct>().HasData(new OrderProduct[]
        {
            new OrderProduct 
            { 
                ID = 1,
                OrderId = 1,
                ProductId = 1
            }
        });
    }
}
