using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentTestbuildingLinkSolution.ServiceReference1;
using PaymentTestbuildingLinkSolution.TransactionReportingService;
using WSAccountType = PaymentTestbuildingLinkSolution.ServiceReference1.WSAccountType;
using WSOperationType = PaymentTestbuildingLinkSolution.ServiceReference1.WSOperationType;
using WSPaymentOrigin = PaymentTestbuildingLinkSolution.ServiceReference1.WSPaymentOrigin;
using WSSettlementType = PaymentTestbuildingLinkSolution.ServiceReference1.WSSettlementType;

namespace PaymentTestbuildingLinkSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var entityID = 236682;
                var locationId = "2389798";
                var storeId = 1284498;
                string storeKey = "Uzz4mwCc5217RnVbec+97ajzruEW";
                var terminalNumber = "__WebService";

                /*
                using (var webService = new ServiceReference1.TransactionProcessingSoapClient())
                {

                    var result = webService.TestConnection();
                    var credResponse =
                        webService.TestCredentials(storeId, storeKey, entityID, locationId, terminalNumber);

                    WSTransaction transactionREquest = new WSTransaction()
                    {
                        EntityId = entityID,
                        LocationId = Convert.ToInt32(locationId),
                        PaymentOrigin = WSPaymentOrigin.Mailed_In,
                        AccountType = WSAccountType.Checking,
                        OperationType = WSOperationType.Sale,
                        SettlementType = WSSettlementType.ACH,
                        EffectiveDate = DateTime.Now,
                        Description = "Test",
                        TotalAmount = 1345,
                        TransactionNumber = "TestImage21",
                        Field1 = "",
                        Field2 = "",
                        Field3 = "",
                        CheckMICRLine = "t111016064t 1101335o 1363",
                        CheckMICRSymbolSet = "toad ?",
                        RoutingNumber = "111016065",
                        AccountNumber = "1101336",
                        CheckNumber = "1362",
                        IsBusinessPayment = false,
                        NameOnAccount = "Ann Taylor1",
                        CheckFrontImageBytes_TiffG4 = null,
                        CheckRearImageBytes_TiffG4 = null,
                        PresentmentNumber = 0
                    };

                    var authTranResponse = webService.AuthorizeTransaction(storeId, storeKey, transactionREquest,
                        WSOwnerApplication.Web_Service);
                    // var captureResponse = webService.CaptureTransaction(storeId, storeKey, entityID, Convert.ToInt32(locationId), authTranResponse.ReferenceNumber, transactionREquest.TotalAmount);

                    var gettrsansactionRequest =
                        webService.GetTransaction(storeId, storeKey, entityID, "DLZNL5CKLA4", true);

                }
                */
                using (var reportWebService = new TransactionReportingService.TransactionReportingSoapClient())
                {
                    var report = reportWebService.GetTransactionReport(storeId, storeKey, entityID, new WSDisplayFields[]
                        {
                           WSDisplayFields.__NONE
                        },
                        new int[] { Convert.ToInt32(locationId) }, null, null, WSPaymentType.__NONE, null, null, WSAuthResponseCode.__NONE, TransactionReportingService.WSOperationType.__NONE,
                        DateTime.Now.AddHours(-2), DateTime.Now.AddDays(1), WSReportDateType.Transactions_Created, "0",
                        "10000");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
