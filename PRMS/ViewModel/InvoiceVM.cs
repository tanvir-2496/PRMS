using PRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRMS.ViewModel
{
    public class InvoiceVM
    {
        public Invoice Invoice { get; set; }
        public List<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
