using System.ComponentModel.DataAnnotations;

namespace MicrosoftSqlClientExamples.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal Bedroom { get; set; } // e.g., 2.5
        public decimal Bathroom { get; set; } // e.g., 1.5
        public int Parking { get; set; }
        public string? Description { get; set; }
        public string? Street { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
