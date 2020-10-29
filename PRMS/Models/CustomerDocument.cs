using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Models
{
    public class CustomerDocument : CommonData
    {
        [Key]
        public long CustomerDocumentId { get; set; }
        
        [StringLength(250)]
        //[Required]
        public string DocumentUrl { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public int? ReferanceId { get; set; }

        [StringLength(250)]
        public string Referance { get; set; }

        public long CustomerId { get; set; }
        public virtual CustomerInfo CustomerInfo { get; set; }
    }
}
