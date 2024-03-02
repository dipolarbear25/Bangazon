using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<BangazonDbContext>(builder.Configuration["BangazonDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/users", (BangazonDbContext db) =>
{
    return db.Users.ToList();
});

app.MapGet("/api/users/{id}", (BangazonDbContext db, int id) =>
{
    var uID = db.Users.FirstOrDefault(u => u.ID == id);

    if (uID == null)
    {
        return Results.NotFound("User not found.");
    }
    return Results.Ok(uID);
});

app.MapPost("/api/users", (BangazonDbContext db, User users) =>
{
    db.Users.Add(users);
    db.SaveChanges();
    return Results.Created($"/api/users/{users.ID}", users);
});

app.MapPut("/api/users/{id}", (BangazonDbContext db, int id, User updateUser) =>
{
    User userToUpdate = db.Users.SingleOrDefault(user => user.ID == id);

    if (userToUpdate == null)
    {
        return Results.NotFound();
    }

    userToUpdate.Email = updateUser.Email;
    userToUpdate.Name = updateUser.Name;
    userToUpdate.IsSeller = updateUser.IsSeller;

    db.SaveChanges();
    return Results.NoContent();
});

app.MapGet("/api/products", (BangazonDbContext db) =>
{
    return db.Products.ToList();
});

app.MapGet("/api/products/{id}", (BangazonDbContext db, int id) =>
{
    var pID = db.Products.FirstOrDefault(u => u.ID == id);

    if (pID == null)
    {
        return Results.NotFound("User not found.");
    }
    return Results.Ok(pID);
});

app.MapGet("/api/products/seller", (BangazonDbContext db, int sellerID) =>
{
    var prodSeller = db.Products.Where(c => c.SellerId == sellerID).ToList();


    if (prodSeller == null)
    {
        return Results.NotFound("No products found by seller.");
    }
    return Results.Ok(prodSeller);
});

app.MapPost("/api/products", (BangazonDbContext db, Product newProduct) =>
{
    db.Products.Add(newProduct);
    db.SaveChanges();
    return Results.Created($"/api/products/{newProduct.ID}", newProduct);
});

app.MapPut("/api/products/{id}", (BangazonDbContext db, int id, Product updateProduct) =>
{
    Product productUpdated = db.Products.SingleOrDefault(product => product.ID == id);

    if (productUpdated == null)
    {
        return Results.NotFound();
    }

    productUpdated.ProductName = updateProduct.ProductName;
    productUpdated.CategoryId = updateProduct.CategoryId;
    productUpdated.QuantityInStock = updateProduct.QuantityInStock;
    productUpdated.Description = updateProduct.Description;
    productUpdated.PricePerUnit = updateProduct.PricePerUnit;

    db.SaveChanges();
    return Results.NoContent();
});

app.MapGet("/api/orders", (BangazonDbContext db) =>
{
    return db.Orders.ToList();
});

app.MapGet("/api/orders/{id}", (BangazonDbContext db, int id) =>
{
    var orderID = db.Orders.FirstOrDefault(c => c.ID == id);

    if (orderID == null)
    {
        return Results.NotFound("Order wasn't found.");
    }

    return Results.Ok(orderID);
});

app.MapPost("/api/orders", (BangazonDbContext db, Order newOrder) =>
{
    db.Orders.Add(newOrder);
    db.SaveChanges();
    return Results.Created($"/api/orders/{newOrder.ID}", newOrder);
});

app.MapPut("/api/orders/{id}", (BangazonDbContext db, int id, Order updateOrder) =>
{
    Order orderUpdated = db.Orders.SingleOrDefault(product => product.ID == id);

    if (orderUpdated == null)
    {
        return Results.NotFound();
    }

    orderUpdated.OrderIsOpen = updateOrder.OrderIsOpen;
    orderUpdated.OrderDate = updateOrder.OrderDate;
    orderUpdated.PaymentType = updateOrder.PaymentType;

    db.SaveChanges();
    return Results.NoContent();
});

app.MapDelete("/api/orderProducts/{id}", (BangazonDbContext db, int id) =>
{
    OrderProduct deleteOrderProducts = db.OrderProducts.SingleOrDefault(orderProductToDelete => orderProductToDelete.ID == id);
    if (deleteOrderProducts == null)
    {
        return Results.NotFound();
    }
    db.OrderProducts.Remove(deleteOrderProducts);
    db.SaveChanges();
    return Results.NoContent();
});

app.MapGet("/api/categories", (BangazonDbContext db) =>
{
    return db.Categories.ToList();
});

app.MapGet("/api/products/category", (BangazonDbContext db, int cateID) =>
{
    var prodCate = db.Products.Where(c => c.CategoryId == cateID).ToList();


    if (prodCate == null)
    {
        return Results.NotFound("No products found by seller.");
    }
    return Results.Ok(prodCate);
});

app.MapGet("/api/orders/user", (BangazonDbContext db, int userID) =>
{
    var userOrder = db.Orders.Where(c => c.UserId == userID).ToList();


    if (userOrder == null)
    {
        return Results.NotFound("No products found by seller.");
    }
    return Results.Ok(userOrder);
});

app.MapGet("/api/orderProducts", (BangazonDbContext db) =>
{
    return db.OrderProducts
    .Include(r => r.Order)
    .Include(r => r.Product)
    .ToList();
});

app.MapGet("/api/products/{id}", (BangazonDbContext db, int id) =>
{
    var prodBought = db.OrderProducts.FirstOrDefault(c => c.ProductId == id);

    if (prodBought == null)
    {
        return Results.NotFound("Order wasn't found.");
    }

    return Results.Ok(prodBought);
});

app.Run();
