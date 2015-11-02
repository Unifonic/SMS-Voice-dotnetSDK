using System;
using System.Collections.Generic;

namespace Unifonic.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var applicationSid = "Your Application Sid";
            var urc = new UnifonicRestClient(applicationSid);

            Console.WriteLine("Call GetBalance");
            var getBalanceresult = urc.GetBalance();
            Console.WriteLine("Balance is:" + getBalanceresult.Balance);

            //Sender ID must be approved by our system before usage
            Console.WriteLine("Call AddSender");
            AddSenderResult addSenderResult = urc.AddSender("test");
            Console.WriteLine("Status of new sender name : " + addSenderResult.Status);


            Console.WriteLine("Call GetSenderStatus");
            GetSenderStatusResult getSenders = urc.GetSenderStatus("SenderSMS");
            Console.WriteLine("Status of Sender name 'SenderSMS' : " + getSenders.Status);

            Console.WriteLine("Call GetSenders");
            List<Sender> getSendersResult = urc.GetSenders();
            Console.WriteLine("You have {0} senders", getSendersResult.Count);
            for (int index = 0; index < getSendersResult.Count; index++)
            {
                Console.WriteLine("{0} : {1}", index + 1, getSendersResult[index].SenderID);
            }

            Console.WriteLine("Call DeleteSender");
            urc.DeleteSender("test");
            Console.WriteLine("Sender 'test' has been deleted successfully");

            Console.WriteLine("Call GetAppDefaultSender");
            var getAppDefaultSenderResult = urc.GetAppDefaultSender();
            Console.WriteLine("Default Sender name is:" + getAppDefaultSenderResult.SenderID);

            Console.WriteLine("Call ChangeAppDefaultSenderId");
            urc.ChangeAppDefaultSender("966000000000");
            Console.WriteLine("Default sender name has been changed successfully");

            //Sender ID must be approved by our system before usage
            Console.WriteLine("Call SendMessage");
            var sendSmsMessageResult = urc.SendSmsMessage("966000000000", "Test");
            Console.WriteLine("Message ID: {0} , Cost: {1} {2},Status: {3}", sendSmsMessageResult.MessageID,
            sendSmsMessageResult.Cost, sendSmsMessageResult.CurrencyCode, sendSmsMessageResult.Status);

            Console.WriteLine("Call SendBulkMessages");
            var sendBulkSmsMessagesResult = urc.SendBulkSmsMessages("966000000000,962799999999", "Test");
            Console.WriteLine("Total number of messages: " + sendBulkSmsMessagesResult.Messages.Count);
            foreach (var msg in sendBulkSmsMessagesResult.Messages)
            {
                Console.WriteLine("Message ID: {0} , Status: {1}", msg.MessageID, msg.Status);
            }

            Console.WriteLine("Call GetMessageStatus");
            var getSmsMessageStatusResult = urc.GetSmsMessageStatus("123456789");
            Console.WriteLine("Message Status is: " + getSmsMessageStatusResult.Status);

            Console.WriteLine("Call GetMessagesReport");
            var getSmsMessagesReportResult = urc.GetSmsMessagesReport(status: SmsMessageStatus.Queued);
            Console.WriteLine("Total number of messages: " + getSmsMessagesReportResult.TotalTextMessages);

            Console.WriteLine("Call GetMessageStatus");
            var getSmsMessagesDetailsResult = urc.GetSmsMessagesDetails();
            Console.WriteLine("Total number of messages: " + getSmsMessagesDetailsResult.Messages.Count);
            foreach (var msg in getSmsMessagesDetailsResult.Messages)
            {
                Console.WriteLine("Message ID: {0} , Cost: {1} {2},Status: {3}", msg.MessageID,
                    msg.Cost, getSmsMessagesDetailsResult.CurrencyCode, msg.Status);
            }

            Console.WriteLine("Call VoiceCall");
            var callResult = urc.Call("966000000000", new Uri(
                "https://voiceusa.s3.amazonaws.com/voiceWavFiles1423399184883.wav"),timeScheduled: DateTime.Now.AddDays(1));
            Console.WriteLine("Call ID: {0} , Cost: {1} , Status: {2}", callResult.CallID,
               callResult.Cost, callResult.CallStatus);

            Console.WriteLine("Call GetCallIdStatus");
            var getCallStatusResult = urc.GetCallStatus("871");
            Console.WriteLine("Cost: {0} , Status: {1}", getCallStatusResult.Price, getCallStatusResult.CallStatus);

            Console.WriteLine("Call GetCallsDetails");
            var getCallsDetailsResult = urc.GetCallsDetails();
            Console.WriteLine("Total number of calls: " + getCallsDetailsResult.Calls.Count);
            foreach (var call in getCallsDetailsResult.Calls)
            {
                Console.WriteLine("Call ID: {0} , Status: {1}", call.CallID, call.CallStatus);
            }

            Console.WriteLine("Call TtsCall");
            var ttsCallResult = urc.TtsCall("966000000000", "welcome", TtsCallLanguages.English, timeScheduled: DateTime.UtcNow.AddDays(1));
            Console.WriteLine("Call ID: {0} , Cost: {1} , Status: {2}", ttsCallResult.CallID,
               ttsCallResult.Cost, ttsCallResult.CallStatus);

            Console.WriteLine("Call NumberInsight");
            var numberInsightResult = urc.NumberInsight("966000000000");
            Console.WriteLine("Status of the number 966000000000 is : {0}", numberInsightResult.SubscriberStatus);


            Console.WriteLine("Call MessagesInbox");
            var messagesInboxResult = urc.MessagesInbox("1200000012");
            Console.WriteLine("Number of messages is : {0}", messagesInboxResult.NumberOfMessages);

            Console.WriteLine("Call VoiceInbox");
            var voiceInboxResult = urc.VoiceInbox("12132944430");
            Console.WriteLine("Number of calls is : {0}", voiceInboxResult.TotalCalls.Count);

            Console.WriteLine("Call MessagesKeyword");
            urc.MessagesKeyword("12132944430", "Test", KeywordRules.Is);
            Console.WriteLine("Keyword added successfully");

            Console.WriteLine("Call GetScheduledMessages");
            var scheduledMessagesResult = urc.GetScheduledMessages();
            Console.WriteLine("Number of scheduled messages is {0}", scheduledMessagesResult.TotalTextMessages);

            Console.WriteLine("Call StopScheduledMessages");
            urc.StopScheduledMessage("96");
            Console.WriteLine("Scheduled message has been canceled");

            Console.WriteLine("Call GetScheduledCalls");
            var getScheduledCallsResult = urc.GetScheduledCalls();
            Console.WriteLine("Number of scheduled calls is {0}", getScheduledCallsResult.TotalVoiceCalls);

            Console.WriteLine("Call StopScheduledCalls");
            urc.StopScheduledCalls("46");
            Console.WriteLine("Scheduled call has been canceled");

            Console.WriteLine("Call SendVerificationCode");
            urc.SendVerificationCode("966000000000", "your code is", securityType: VerificationSecurityType.OTP);
            Console.WriteLine("Verification code has been sent to recipient");

            Console.WriteLine("Call VerifyNumber");
            var verifyNumberResult = urc.VerifyNumber("966000000000", "4865");
            Console.WriteLine("Verification status is : {0}", verifyNumberResult.VerifyStatus);

            Console.WriteLine("Call MessagesPricing");
            var messagesPricing = urc.MessagesPricing("SA");
            Console.WriteLine("Number of operators in Saudi Arabia is {0}", messagesPricing[0].Operators.Count);

            Console.WriteLine("Call GetVerificationDetails");
            var getVerificationDetailsResult = urc.GetVerificationDetails();
            Console.WriteLine("Number of verification messages is {0}", getVerificationDetailsResult.NumberOfMessages);


        }
    }
}
