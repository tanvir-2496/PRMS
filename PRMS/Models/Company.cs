using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Models
{
    public class Company
    {
        [Key]
        public long CompanyId { get; set; }

        [Required(ErrorMessage ="Please Enter Company Name")]
        [StringLength(100, MinimumLength = 2)]
        public string CompanyName { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Please Enter Company Name")]
        public string Mobile { get; set; }

        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Address { get; set; }
    }
}
