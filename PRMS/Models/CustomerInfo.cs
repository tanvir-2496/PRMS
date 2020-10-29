using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Models
{
    public class CustomerInfo : CommonData
    {
        [Key]
        public long CustomerId { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage ="Enter Customer First Name")]
        public string FirstName { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Enter Customer Last Name")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Enter Customer Father's Name")]
        public string FatherName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Enter Customer Mother's Name")]
        public string MotherName { get; set; }

        [StringLength(50)]
        public string SpouseName { get; set; }

        [StringLength(30)]
        public string NID { get; set; }

        [StringLength(30)]
        public string BirthId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(20)]
        public string Contact1 { get; set; }

        [StringLength(20)]
        public string Contact2 { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(250)]
        public string CustomerImage { get; set; }

        [StringLength(250)]
        public string PresentAddress { get; set; }

        [StringLength(250)]
        public string PermanentAddress { get; set; }

        public virtual ICollection<Aggrement> Aggrement { get; set; }
        public virtual ICollection<CustomerDocument> CustomerDocument { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
