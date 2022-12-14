using FluentValidation.AspNetCore;
using ScoreBoardMgm.API.Validators;
using AutoMapper;
using ScorecardMgm.Common.Entities;
using Microsoft.EntityFrameworkCore;
using ScorecardMgm.API.Interfaces;
using ScorecardMgm.API.Implementations;
using ScorecardMgm.API.Middlewares;
using ScorecardMgm.API.Models.Response;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Endpoints>(builder.Configuration.GetSection("Endpoints"));

builder.Services.AddTransient<IOverService, OverService>();
builder.Services.AddTransient<ScorecardMgm.Common.Repositories.Interfaces.IOverRepository, ScorecardMgm.Common.Repositories.Implementations.OverRepository>();
builder.Services.AddTransient<IMatchService, MatchService>();
builder.Services.AddTransient<ScorecardMgm.Common.Repositories.Interfaces.IMatchRepository, ScorecardMgm.Common.Repositories.Implementations.MatchRepository>();
builder.Services.AddTransient<ITournamentService, TournamentService>();
builder.Services.AddTransient<ScorecardMgm.Common.Repositories.Interfaces.ITournamentRepository, ScorecardMgm.Common.Repositories.Implementations.TournamentRepository>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<ScorecardMgm.Common.Repositories.Interfaces.ITeamRepository, ScorecardMgm.Common.Repositories.Implementations.TeamRepository>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<ScorecardMgm.Common.Repositories.Interfaces.IPlayerRepository, ScorecardMgm.Common.Repositories.Implementations.PlayerRepository>();



builder.Services
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<MatchValidator>();
        config.RegisterValidatorsFromAssemblyContaining<PlayerValidator>();
        config.RegisterValidatorsFromAssemblyContaining<OverValidator>();
        config.RegisterValidatorsFromAssemblyContaining<TeamValidator>();
        config.RegisterValidatorsFromAssemblyContaining<TournamentValidator>();
    });


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddDbContext<ScorecardMgmContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("express")));

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
//                 .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
//             ValidateIssuer = false,
//             ValidateAudience = false
//         };
//     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<TokenValidationMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
