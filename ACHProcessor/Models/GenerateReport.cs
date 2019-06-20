using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACHProcessor.Models
{
	public class GenerateReport
	{
		public string CustomerNumber { get; set; }
		public string AccountNumber { get; set; }
		public string NameOnAccount { get; set; }
		public string TransactionNumber { get; set; }
		public string OperationType { get; set; }
		public string PaymentType { get; set; }
		public decimal TotalAmount { get; set; }
		public DateTime TransactionDateTime { get; set; }
		public string TransactionStatus { get; set; }
	}
}