using System.ComponentModel.DataAnnotations;

namespace TravelAdvisor.Backend.Models
{
    public class Image
    {
        public Guid Id { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string ContentType { get; set; } = string.Empty;

        [Required]
        public byte[] Data { get; set; } = Array.Empty<byte>();

        public Guid PlaceId { get; set; }
        public Place Place { get; set; } = null!;
    }
} 