using System;
using System.Collections.Generic;

namespace Otsdc.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var orc = new OtsdcRestClient("Your Application Sid");

            Console.WriteLine("Call GetBalance");
            var getBalanceresult = orc.GetBalance();
            Console.WriteLine("Balance is:" + getBalanceresult.Balance);
            
            //The sider ID must be approved by our system
            Console.WriteLine("Call AddSender");
            AddSenderResult addSenderResult = orc.AddSender("test");
            Console.WriteLine("Status of new sender name : " + addSenderResult.Status);
            
            
            Console.WriteLine("Call GetSenderStatus");
            GetSenderStatusResult getSenders = orc.GetSenderStatus("SenderSMS");
            Console.WriteLine("Status of Sender name 'SenderSMS' : " + getSenders.Status);

            Console.WriteLine("Call GetSenders");
            List<Sender> getSendersResult = orc.GetSenders();
            Console.WriteLine("You have {0} senders", getSendersResult.Count);
            for (int index = 0; index < getSendersResult.Count; index++)
            {
                Console.WriteLine("{0} : {1}", index + 1, getSendersResult[index].SenderID);
            }

            Console.WriteLine("Call DeleteSender");
            orc.DeleteSender("test");
            Console.WriteLine("Sender 'test' has been deleted successfully");

            Console.WriteLine("Call GetAppDefaultSender");
            var getAppDefaultSenderResult = orc.GetAppDefaultSender();
            Console.WriteLine("Default Sender name is:" + getAppDefaultSenderResult.SenderID);

            Console.WriteLine("Call ChangeAppDefaultSenderId");
            orc.ChangeAppDefaultSender("962788888888");
            Console.WriteLine("Default sender name has been changed successfully");

            //The sider ID must be approved by our system
            Console.WriteLine("Call SendMessage");
            var sendSmsMessageResult = orc.SendSmsMessage("962788888888", "Test");
            Console.WriteLine("Message ID: {0} , Cost: {1} {2},Status: {3}", sendSmsMessageResult.MessageID,
            sendSmsMessageResult.Cost, sendSmsMessageResult.CurrencyCode, sendSmsMessageResult.Status);

            //Sned bulk feature must be added to be used 
            //Please contact our support team if the feature is not added to your account yet
            Console.WriteLine("Call SendBulkMessages");
            var sendBulkSmsMessagesResult = orc.SendBulkSmsMessages("962788888888,962799999999", "Test");
            Console.WriteLine("Total number of messages: " + sendBulkSmsMessagesResult.Messages.Count);
            foreach (var msg in sendBulkSmsMessagesResult.Messages)
            {
                Console.WriteLine("Message ID: {0} , Status: {1}", msg.MessageID, msg.Status);
            }

            Console.WriteLine("Call GetMessageStatus");
            var getSmsMessageStatusResult = orc.GetSmsMessageStatus("123456789");
            Console.WriteLine("Message Status is: " + getSmsMessageStatusResult.Status);

            Console.WriteLine("Call GetMessagesReport");
            var getSmsMessagesReportResult = orc.GetSmsMessagesReport(status: SmsMessageStatus.Queued);
            Console.WriteLine("Total number of messages: " + getSmsMessagesReportResult.TotalTextMessages);

            Console.WriteLine("Call GetMessageStatus");
            var getSmsMessagesDetailsResult = orc.GetSmsMessagesDetails();
            Console.WriteLine("Total number of messages: " + getSmsMessagesDetailsResult.Messages.Count);
            foreach (var msg in getSmsMessagesDetailsResult.Messages)
            {
                Console.WriteLine("Message ID: {0} , Cost: {1} {2},Status: {3}", msg.MessageID,
                    msg.Cost, getSmsMessagesDetailsResult.CurrencyCode, msg.Status);
            }

            //TODO: add CurrencyCode
            Console.WriteLine("Call VoiceCall");
            var callResult = orc.Call("962788888888", new Uri(
                "https://voiceusa.s3.amazonaws.com/voiceWavFiles1423399184883.wav"));
            Console.WriteLine("Call ID: {0} , Cost: {1} , Status: {2}", callResult.CallID,
               callResult.Cost, callResult.CallStatus);

            Console.WriteLine("Call GetCallIdStatus");
            var getCallStatusResult = orc.GetCallStatus("102");
            Console.WriteLine("Cost: {0} , Status: {1}", getCallStatusResult.Price, getCallStatusResult.CallStatus);

            Console.WriteLine("Call GetCallsDetails");
            var getCallsDetailsResult = orc.GetCallsDetails();
            Console.WriteLine("Total number of calls: " + getCallsDetailsResult.Calls.Count);
            foreach (var call in getCallsDetailsResult.Calls)
            {
                Console.WriteLine("Call ID: {0} , Status: {1}", call.CallID, call.CallStatus);
            }

            Console.WriteLine("Call TtsCall");
            var ttsCallResult = orc.TtsCall("962788888888", "welcome", TtsCallLanguages.English);
            Console.WriteLine("Call ID: {0} , Cost: {1} , Status: {2}", ttsCallResult.CallID,
               ttsCallResult.Price, ttsCallResult.CallStatus);

            Console.WriteLine("Call SendEmail");
            var sendEmailResult = orc.SendEmail("from@domain.com", "to@domain.com", "This is a test");
            Console.WriteLine("Email ID: {0} , Cost: {1} , Status: {2}", sendEmailResult.EmailID,
              sendEmailResult.Cost, sendEmailResult.EmailStatus);

            Console.WriteLine("Call GetEmailsReport");
            var getEmailsReportResult = orc.GetEmailsReport();
            Console.WriteLine("Total number of email: " + getEmailsReportResult.TotalEmails);
           

        }
    }
}
