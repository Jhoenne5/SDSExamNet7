using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDSExamNet7.Models.Entities
{
    public class RecyclableType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public decimal Rate { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public decimal MinKg { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public decimal MaxKg { get; set; }

        public ICollection<RecyclableItem> RecyclableItems { get; set; }
    }
}
