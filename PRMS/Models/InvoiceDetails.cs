using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Models
{
    public class InvoiceDetails : CommonData
    {
        [Key]
        public long InvoiceDetailsId { get; set; }

        [ForeignKey("Invoice")]
        public long? InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        [ForeignKey("InventoryItem")]
        public long? ItemId { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }

        [StringLength(30)]
        public string PaidMonthYear { get; set; }

        [StringLength(20)]
        // Cash, Check, Mobile Banking or Others Type
        public string CollectionType { get; set; }

        public decimal? PaidAmount { get; set; }

        //1 = Not Paid, 2 = Install Payed, 3 = Full Paid
        public int? PaymentStatus { get; set; }
    }
}
