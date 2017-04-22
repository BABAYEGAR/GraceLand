using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data.Objects.Entities
{
    public class Role: Transport
    {
        public long RoleId { get; set; }
        [Required]
        public string Name { get; set; }
        public bool ManageItems { get; set; }
        public bool ManageItemCategory { get; set; }
        public bool ManageUsers { get; set; }
        public bool AccessItemLog { get; set; }
        public bool AccessGeneralLog { get; set; }
    }
}
