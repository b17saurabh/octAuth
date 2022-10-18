namespace ScorecardMgm.Auth.Routes;

public class apiRoutes
{
    public static class User
    {
        public const string Base = "user";
        public const string Register = Base + "/register";
        public const string UpdateUser = Base + "/update/{userId}";
        public const string DeleteUser = Base + "/delete/{userId}";
        public const string GetAllUsers = Base + "/getall";

        public const string GetUserById = Base + "/getbyid/{userId}";
    }

    public static class Auth
    {
        public const string Base = "auth";
        public const string Login = Base + "/login";
    }
}

