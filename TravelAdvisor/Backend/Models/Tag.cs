using System.ComponentModel.DataAnnotations;

namespace TravelAdvisor.Backend.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
} 