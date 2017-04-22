using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Data.Objects.Entities
{
    public class ItemLog : Transport
    {
        public long ItemLogId { get; set; }
        public long? ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public string TransactionType { get; set; }
        public long Quantity { get; set; }
        public long TotalAmount { get; set; }
    }
}
