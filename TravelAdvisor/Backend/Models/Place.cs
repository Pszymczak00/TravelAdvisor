using System.ComponentModel.DataAnnotations;

namespace TravelAdvisor.Backend.Models
{
    public class Place
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public string Street { get; set; } = string.Empty;

        [Required]
        public string StreetNumber { get; set; } = string.Empty;

        [Required]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public TimeSpan ExpectedVisitTime { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<OpeningHours> OpeningHours { get; set; } = new List<OpeningHours>();

        public ICollection<Image> Images { get; set; } = new List<Image>();

        public ICollection<UserPlaceOpinion> UserOpinions { get; set; } = new List<UserPlaceOpinion>();
    }
} 