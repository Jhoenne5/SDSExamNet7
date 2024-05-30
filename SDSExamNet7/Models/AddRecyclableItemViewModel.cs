using SDSExamNet7.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SDSExamNet7.Models
{
    public class AddRecyclableItemViewModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RecyclableType")]
        public int RecyclableTypeId { get; set; }
        public RecyclableType RecyclableType { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public decimal Weight { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public decimal ComputedRate { get; set; }

        [StringLength(150)]
        public string ItemDescription { get; set; }
    }
}
