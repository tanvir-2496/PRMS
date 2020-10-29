using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Models
{
    public class Expance: CommonData
    {
        [Key]
        public long ExpanceId { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpanceDate { get; set; }

        [Required(ErrorMessage = "Expance Type is required")]
        [StringLength(30)]
        //Client = 1, House = 2, Shop = 3, Month and Year = 4 and Others = 5
        public string ExpanceType { get; set; }

        public long? InventoryItemId { get; set; }

        public long? CustomerId { get; set; }

        public string MonthYear { get; set; }

        [Required(ErrorMessage = "Please Enter Amount")]
        public decimal Amount { get; set; }

        [NotMapped]
        public string CustomerName { get; set; }

        [NotMapped]
        public string ItemName { get; set; }
    }
}
