using FileWatcherServer.Data;
using FileWatcherServer.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<ApiDbContext>(option =>
{
    option
    .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FileWatcherServer;Trusted_Connection=True;MultipleActiveResultSets=true")
    .UseLazyLoadingProxies();
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 3;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireDigit = false;
})
  .AddEntityFrameworkStores<ApiDbContext>()
  .AddDefaultTokenProviders();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "http://www.security.org",
        ValidIssuer = "http://www.security.org",
        IssuerSigningKey = new SymmetricSecurityKey
      (Encoding.UTF8.GetBytes("vmihosszukodajwttokenhez"))
    };
});
builder.Services.AddAuthorization();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHub<EventHub>("/events");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
