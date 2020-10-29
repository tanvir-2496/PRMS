using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Models
{
    public class Aggrement : CommonData
    {
        [Key]
        public long AggrementId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? AggrementDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AggrementStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AggrementEndDate { get; set; }

        public decimal? AggrementAmount { get; set; }

        public decimal? MonthlyRent { get; set; }

        public long? InventoryItemId { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }

        public long? CustomerId { get; set; }
        public virtual CustomerInfo CustomerInfo { get; set; }
    }
}
