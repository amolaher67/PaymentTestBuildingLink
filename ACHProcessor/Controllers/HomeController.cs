using ACHPaymentWeb.Models;
using ACHProcessor.Models;
using ACHProcessor.TransactionProcessingService;
using ACHProcessor.TrsansactionReportingService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACHProcessor.Controllers
{
	public class HomeController : Controller
	{
		public int entityID = 236682;
		public string locationId = "2389798";
		public int storeId = 1284498;
		public string storeKey = "Uzz4mwCc5217RnVbec+97ajzruEW";
		public string terminalNumber = "__WebService";

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		[HttpGet]
		public ActionResult MakePayment()
		{
			return View();
		}

		[HttpPost]
		public JsonResult MakePayment(MakePayment makePayment)
		{
			try
			{
				using (var webService = new TransactionProcessingService.TransactionProcessingSoapClient())
				{

					var result = webService.TestConnection();
					var credResponse =
						webService.TestCredentials(storeId, storeKey, entityID, locationId, terminalNumber);

					WSTransaction transactionRequest = new WSTransaction()
					{
						EntityId = entityID,
						LocationId = Convert.ToInt32(locationId),
						PaymentOrigin = TransactionProcessingService.WSPaymentOrigin.Internet,
						AccountType = TransactionProcessingService.WSAccountType.Checking,
						OperationType = TransactionProcessingService.WSOperationType.Sale,
						SettlementType = TransactionProcessingService.WSSettlementType.ACH,
						EffectiveDate = DateTime.Now,
						Description = "Test",
						TotalAmount = makePayment.TotalAmount,
						TransactionNumber = "TestImage75",
						Field1 = "",
						Field2 = "",
						Field3 = "",
						//CheckMICRLine = "",
						//CheckMICRSymbolSet = "toad ?",
						RoutingNumber = makePayment.RoutingNumber,
						AccountNumber = makePayment.AccountNumber,
						CheckNumber = "1362",
						IsBusinessPayment = makePayment.IsBusinessPayment,
						NameOnAccount = makePayment.NameOnAccount,
						CheckFrontImageBytes_TiffG4 = null,
						CheckRearImageBytes_TiffG4 = null,
						PresentmentNumber = 0
					};

					var authTranResponse = webService.AuthorizeTransaction(storeId, storeKey, transactionRequest, WSOwnerApplication.Web_Service);

					return Json(authTranResponse);
				}
			}
			catch (Exception e)
			{
				throw e;
			}
		}


		public ActionResult GenerateReport()
		{
			List<TransactionReport> reportList = new List<TransactionReport>();
			try
			{


				using (var reportWebService = new TrsansactionReportingService.TransactionReportingSoapClient())
				{
					var reports = reportWebService.GetTransactionReport(storeId, storeKey, entityID, new WSDisplayFields[]
						{
							WSDisplayFields.__NONE
						},
						new int[] { Convert.ToInt32(locationId) }, null, null, WSPaymentType.__NONE, null, null, WSAuthResponseCode.__NONE, TrsansactionReportingService.WSOperationType.__NONE,
						DateTime.Now.AddHours(-2), DateTime.Now.AddDays(1), WSReportDateType.Transactions_Created, "0",
						"10000");

					foreach (var item in reports)
					{
						reportList.Add(new TransactionReport()
						{
							TransactionreferenceNumber = item.ReferenceNumber,
							TotalAmount = item.TotalAmount.ToString("F2"),
							AcountNumber = item.AccountNumber,
							CustomerName = item.NameOnAccount,
							DateOfTransaction = item.TransactionDateTime,
							TransactionStatus = item.TransactionStatus
						});
					}

				}

				return View(reportList);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			//return View();
		}

		[HttpGet]
		public ActionResult VoidTransaction(string referenceNumber)
		{
			try
			{
				if (string.IsNullOrEmpty(referenceNumber))
					return HttpNotFound("Reference NUmber should not be empty");

				using (var webService = new TransactionProcessingService.TransactionProcessingSoapClient())
				{

					var result = webService.TestConnection();
					var credResponse =
						webService.TestCredentials(storeId, storeKey, entityID, locationId, terminalNumber);


					//it will removed fromm transaction pproceessing
					var voidTransactionresponse = webService.VoidTransaction(storeId, storeKey, entityID, Convert.ToInt32(locationId), referenceNumber);

					@ViewBag.Message = voidTransactionresponse.ResponseCode;
				}

				return View();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}