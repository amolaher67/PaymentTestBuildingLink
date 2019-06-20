using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACHProcessor.Models
{
	//Mrunali Pathak : 20/06/2019
	//Make Payment Model 
	public class MakePayment
	{
		//public string TransactionNumber { get; set; }
		[Required(ErrorMessage ="Please Enter Account Number")]
		[StringLength(11,ErrorMessage ="Account Number exceding the length")]
		[RegularExpression("^[0-9]*$", ErrorMessage = "Account Number must be numeric")]
		public string AccountNumber { get; set; }
		
		[Required(ErrorMessage = "Please select Account Type")]
		public string AccountType { get; set; }
		//public string PaymentOrigin { get; set; }
		//public string SettlementType { get; set; }
		//public string OperationType { get; set; }
		public decimal TotalAmount { get; set; }
		//public string CheckMICRLine { get; set; }
		[Required]
		[StringLength(9, ErrorMessage = "Account Number exceding the length")]
		[RegularExpression("^[0-9]*$", ErrorMessage = "Routing Number must be numeric")]
		public string RoutingNumber { get; set; }

		[Required(ErrorMessage ="Please Enter Name on Account")]
		public string NameOnAccount { get; set; }

		[Required(ErrorMessage ="Please Enter Presentment Number")]
		public int PresentmentNumber { get; set; }

		[Required(ErrorMessage = "Please select Business Payment")]
		public bool IsBusinessPayment { get; set; }
	}
}