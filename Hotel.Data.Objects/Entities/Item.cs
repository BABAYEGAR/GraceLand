using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data.Objects.Entities
{
    public class Item :  Transport
    {
        public long ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
        public long Amount { get; set; }
        public long? ItemCategoryId { get; set; }
        [ForeignKey("ItemCategoryId")]
        public ItemCategory ItemCategory { get; set; }

    }
}
