using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using quntrixAPI;
using quntrixAPI.Endpoints;
using quntrixAPI.EndPoints;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Http;
using System.Text;
//using Front.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<quntrixAPI.AppDbContext>(options =>
    options.UseSqlServer(connectionString));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description ="standart authorization header using the Bearer Scheme (\"bearer {token}\")",
        In= ParameterLocation.Header,
        Name="Authorization",
        Type = SecuritySchemeType.ApiKey
        });
    options.OperationFilter<SecurityRequirementsOperationFilter>();

});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer =false,
        ValidateAudience =false
    };
}); 


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapSpeakerEndpoints();
app.MapAttendeeEndpoints();
app.MapSessionEndpoints();



app.UseHttpsRedirection();



app.MapControllers();

app.Run();
