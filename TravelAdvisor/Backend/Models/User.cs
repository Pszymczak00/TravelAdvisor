using System.ComponentModel.DataAnnotations;

namespace TravelAdvisor.Backend.Models
{
    public class User
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public ICollection<UserPlaceOpinion> Opinions { get; set; } = new List<UserPlaceOpinion>();
    }
} 