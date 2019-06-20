using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACHPaymentWeb.Models
{
    public class TransactionReport
    {
        public  string TransactionreferenceNumber { get; set; }
        public string TotalAmount { get; set; }
        public string AcountNumber { get; set; }
        public string TransactionStatus { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public  string CustomerName { get; set; }
    }
}