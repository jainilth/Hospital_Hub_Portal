using CloudinaryDotNet;
using Hospital_Hub_Portal;
using Hospital_Hub_Portal.Hubs;
using Hospital_Hub_Portal.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HospitalHubContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins(["http://localhost:5173", "http://localhost:5174"])
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
});

builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());

builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings")
);

builder.Services.AddSingleton(provider =>
{
    var config = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
    var account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
    return new Cloudinary(account);
});

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowReactApp");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<VideoCallHub>("/chat");
});



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
