using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MySql.Data.MySqlClient;
using Server.Data;
using Onlineauction.Interfaces;
using Server.Repository;
using Microsoft.OpenApi.Models;
using Server.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddCookie("opa23.onlineauction.cars");
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin_route", policy => policy.RequireRole("admin"));
    options.AddPolicy("user_route", policy => policy.RequireRole("user"));
});

// Add MVC services to the container
builder.Services.AddControllers();

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Online Auction API",
        Description = "API for Online Auction"
    });
});

string connectionString = "server=localhost;uid=root;pwd=mypassword;database=onlineauction;port=3306";
builder.Services.AddScoped(_ => new ApplicationDbContext(connectionString)); /// <-- NEW 

// Adding Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();

//////builder.WebHost.ConfigureKestrel(serverOptions =>
//////{
//////    serverOptions.ListenAnyIP(3008);
//////});

try
{
    var app = builder.Build();

    var distPath = Path.Combine(app.Environment.ContentRootPath, "./dist");
    var fileProvider = new PhysicalFileProvider(distPath);

    app.UseHttpsRedirection();

    app.UseDefaultFiles(new DefaultFilesOptions
    {
        FileProvider = fileProvider,
        DefaultFileNames = new List<string> { "index.html" }
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = fileProvider,
        RequestPath = ""
    });

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve Swagger-UI (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Auction API V1");
        c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
    });

    app.MapControllers();

    app.Run();
}
catch (MySqlException e)
{
    Console.WriteLine(e);
}


