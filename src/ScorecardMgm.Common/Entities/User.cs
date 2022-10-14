using System.ComponentModel.DataAnnotations;

namespace ScorecardMgm.Common.Entities
{
    public class User
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int Role { get; set; }
    }
}