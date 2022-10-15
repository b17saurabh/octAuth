

namespace ScorecardMgm.API.APIroutes
{
    public static class Routes
    {
        public static class Match
        {
            public const string Base = "match";
            public const string Get = Base + "/{matchid}";
            public const string Delete = Base + "/{matchid}";
            public const string Update = Base + "/{matchid}";
            public const string Create = Base + "/{tournamentId}";
            public const string GetAll = Base;
        }

        public static class Over
        {
            public const string Base = "over";
            public const string Get = Base + "/{overid}";
            public const string Delete = Base + "/{overid}";
            public const string Update = Base + "/{overid}";
            public const string Create = Base + "/{matchId}";
            public const string GetAll = Base;
        }

        public static class Player
        {
            public const string Base = "player";
            public const string Get = Base + "/{playerid}";
            public const string Delete = Base + "/{playerid}";
            public const string Update = Base + "/{playerid}";
            public const string Create = Base + "/{teamId}";
            public const string GetAll = Base;
        }

        public static class Team
        {
            public const string Base = "team";
            public const string Get = Base + "/{teamid}";
            public const string Delete = Base + "/{teamid}";
            public const string Update = Base + "/{teamid}";
            public const string Create = Base;
            public const string GetAll = Base;
        }

        public static class Tournament
        {
            public const string Base = "tournament";
            public const string Get = Base + "/{tournamentid}";
            public const string Delete = Base + "/{tournamentid}";
            public const string Update = Base + "/{tournamentid}";
            public const string Create = Base;
            public const string GetAll = Base;
        }
    }
}