using System.ComponentModel.DataAnnotations;

namespace Hiring.Challenge.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
    }
}
