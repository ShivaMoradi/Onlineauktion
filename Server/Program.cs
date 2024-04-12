using MySql.Data.MySqlClient;
using Onlineauction;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication().AddCookie("opa23.onlineauction.cars");
builder.Services.AddAuthorizationBuilder().AddPolicy("admin_route", policy => policy.RequireRole("admin"));
builder.Services.AddAuthorizationBuilder().AddPolicy("user_route", policy => policy.RequireRole("user"));

string connectionString = "server=localhost;uid=root;pwd=mypassword;database=onlineauction;port=3306";

try
{

    builder.Services.AddSingleton(new State(connectionString));
    var app = builder.Build();

    app.MapPost("/login", Auth.Login);
    app.MapGet("/admin", () => "Hello, Admin!").RequireAuthorization("admin_route");
    app.MapGet("/user", () => "Hello, User!").RequireAuthorization("user_route");

    //users
    app.MapGet("/api/users", Users.All);
    app.MapPost("/api/users", Users.Post);
    app.MapPost("/api/users/user", Users.PostUser);
    app.MapPatch("/api/users/password/{id}", Users.UpdateUserPassword);
    app.MapDelete("/api/users/{id}", Users.DeleteUserId);
    


    //auctions
    app.MapGet("/api/auctions", Auctions.All);
    app.MapGet("/api/auctions/{id}", Auctions.GetAuctionFromId);
    app.MapPost("/api/auctions", Auctions.Post);
    app.MapPatch("/api/auctions/{id}", Auctions.UpdateBidFromAuctionId);
    app.MapPatch("/api/auctions/fromcarid/{carId}", Auctions.UpdateBidFromCarId);
    app.MapDelete("/api/auctions/{id}", Auctions.DeleteAuctionFromId);

    //obtaining cars /api
    app.MapGet("/", Cars.GetCarsHome);
    app.MapGet("/api/cars", Cars.GetAllCars);
    app.MapGet("/api/cars/{id}", Cars.GetCarId);

    //bids
    app.MapGet("/api/bids", Bids.All);
    app.MapPost("/api/bids", Bids.PostBid);


    app.Run("http://localhost:3008");

}
catch (MySqlException e)
{
    Console.WriteLine(e);

}

public record State(string DB);