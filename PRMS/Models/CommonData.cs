using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.Models
{
    public class CommonData
    {
        [StringLength(250)]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [StringLength(250)]
        public string UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }

        [StringLength(100)]
        public string UserIp { get; set; }

        public int Status { get; set; }

        public bool IsRemove { get; set; }

        public long? CompanyId { get; set; }
    }
}
