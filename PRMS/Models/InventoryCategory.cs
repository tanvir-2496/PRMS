using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Models
{
    public class InventoryCategory : CommonData
    {
        [Key]
        public long InventoryCategoryId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Enter Category")]
        public string CategoryName { get; set; }

        [StringLength(100)]
        public string ImageUrl { get; set; }

        public virtual ICollection<InventoryItem> InventoryItem { get; set; }
    }
}
