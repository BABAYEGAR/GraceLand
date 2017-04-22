using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data.Objects.Entities
{
    public class ItemCategory : Transport
    {
        public long ItemCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
