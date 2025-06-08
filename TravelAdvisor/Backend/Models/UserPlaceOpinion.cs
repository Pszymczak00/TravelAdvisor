using System.ComponentModel.DataAnnotations;

namespace TravelAdvisor.Backend.Models
{
    public class UserPlaceOpinion
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid PlaceId { get; set; }
        public Place Place { get; set; } = null!;

        public int PositiveOpinions { get; set; }
        public int NegativeOpinions { get; set; }

        [Required]
        public DateTime LastModified { get; set; }
    }
} 