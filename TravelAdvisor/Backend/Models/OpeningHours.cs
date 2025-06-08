using System.ComponentModel.DataAnnotations;

namespace TravelAdvisor.Backend.Models
{
    public class OpeningHours
    {
        public Guid Id { get; set; }

        public Guid PlaceId { get; set; }
        public Place Place { get; set; } = null!;

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }

        public bool IsClosed { get; set; }
    }
} 