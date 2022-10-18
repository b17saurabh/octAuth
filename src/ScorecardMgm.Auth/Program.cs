
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using ScorecardMgm.Auth.Validator;
using ScorecardMgm.Auth.Services.Implementation;
using ScorecardMgm.Auth.Services.Interface;
using ScorecardMgm.Common.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserServices, UserServices>();
// builder.Services.AddTransient<ScorecardMgm.Common.Repositories.Interfaces.IUserRepository, ScorecardMgm.Common.Repositories.Implementation.UserRepository>();
builder.Services.AddTransient<IAuthServices, AuthServices>();
builder.Services.AddTransient<ScorecardMgm.Common.Repositories.Interfaces.IUserRepository, ScorecardMgm.Common.Repositories.Implementation.UserRepository>();


builder.Services.AddDbContext<ScorecardMgmContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("express")));



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<UserValidator>();
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
