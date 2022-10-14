create database ScoreDBwithAuth
go


create table Users(
id varchar(36) not null Primary key,
Name varchar(16),
Email varchar(25),
PasswordHash varchar(50),
role varchar(5)
)

create table Tournaments(
    TournamentId varchar(36) not null Primary key,
    TournamentName varchar(25),
)

create table Teams(
    TeamId varchar(36) not null Primary key,
    TeamName varchar(25),
)

create table Overs(
    OverId varchar(36) not null Primary key,
    MatchId varchar(36),
    RunCount varchar(5),
    foreign key (MatchId) references Matches(MatchId)
)

create table Matches(
    MatchId varchar(36) not null Primary key,
    MatchName varchar(25),
    TournamentId varchar(36),
    WinnerTeamId varchar(36),
    foreign key (TournamentId) references Tournaments(TournamentId),
    foreign key (WinnerTeamId) references Teams(TeamId),
)

create table Players(
    PlayerId varchar(36) not null Primary key,
    PlayerName varchar(25),
    TeamId varchar(36),
    foreign key (TeamId) references Teams(TeamId),
)
