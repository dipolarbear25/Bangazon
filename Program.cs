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
        return Results.NotFound("User Not Found.");
    }
    return Results.Ok(uID);
});

app.MapPost("/api/users", (BangazonDbContext db, User users) =>
{
    db.Users.Add(users);
    db.SaveChanges();
    return Results.Created($"/api/campsites/{users.ID}", users);
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

app.Run();
