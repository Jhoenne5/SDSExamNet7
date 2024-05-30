using SDSExamNet7.Models.Entities;

namespace SDSExamNet7.Models.DTOS
{
    public class RecyclableItemWithTypeDTO
    {
        public int Id { get; set; }
        public decimal Weight { get; set; }
        public decimal ComputedRate { get; set; }
        public string ItemDescription { get; set; }
        public int RecyclableTypeId { get; set; }
        public string Type { get; set; }
        public decimal Rate { get; set; }
        public decimal MinKg { get; set; }
        public decimal MaxKg { get; set; }
    }


    public class AddRecyclableItemDTO
    {
        public RecyclableItemWithTypeDTO RecyclableItem { get; set; }
        public IEnumerable<RecyclableType> RecyclableTypes { get; set; }
    }

}
