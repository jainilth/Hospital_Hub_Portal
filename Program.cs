using Hospital_Hub_Portal.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HospitalHubContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

<<<<<<< HEAD
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.AllowAnyOrigin()  // <-- React app origin
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});
=======
>>>>>>> develop

var app = builder.Build();
// .NET 6 and later (Program.cs)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowReactApp",
//        policy => policy
//            .WithOrigins("http://localhost:5173") // React dev server
//            .AllowAnyMethod()
//            .AllowAnyHeader());
//});

//app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
