using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Models
{
    public class InventoryItem : CommonData
    {
        [Key]
        public long InventoryItemId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Enter Item")]
        // Like: Shop, Flat, Flot etc
        public string ItemName { get; set; }

        [StringLength(50)]
        public string ItemCode { get; set; }

        [StringLength(250)]
        public string Location { get; set; }

        public long? InventoryCategoryId { get; set; }
        public virtual InventoryCategory InventoryCategory { get; set; }

        public virtual ICollection<Aggrement> Aggrement { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
