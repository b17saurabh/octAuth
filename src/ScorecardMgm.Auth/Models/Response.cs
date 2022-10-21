

namespace ScorecardMgm.Auth.Models
{
    public class Response
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public Response(string token, string userName)
        {
            Token = token;
            Email = userName;
            Message = "Successfully Logged in";
        }
    }
}