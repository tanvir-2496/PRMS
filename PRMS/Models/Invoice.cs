using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Models
{
    public class Invoice : CommonData
    {
        [Key]
        public long InvoiceId { get; set; }

        [StringLength(30)]
        public string InvoiceNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        public decimal? TotalAmount { get; set; }

        [ForeignKey("CustomerInfo")]
        public long? CustomerId { get; set; }
        public virtual CustomerInfo CustomerInfo { get; set; }

        public virtual List<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
