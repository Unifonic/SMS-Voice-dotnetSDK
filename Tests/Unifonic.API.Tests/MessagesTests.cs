using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Globalization;

namespace Unifonic.API.IntegrationTests
{
    public class MessagesTests
    {
        private Mock<UnifonicRestClient> _mockClient;

        private const string OneRecipient = "962788888888";
        private const string ManyRecipients = "962788888888,962796666666";
        private const string Body = ".Net Unit Test Message";
        private const string SenderId = "unitTest";
        private const string MessageId = "123";
        private const string Country = "Jordan";
        private const DlrStatus Dlr = DlrStatus.Delivered;
        private readonly SendSmsType SendSmsType = SendSmsType.Flash;
        private readonly SendPriority SendPriority = SendPriority.High;
        private DateTime SendTimeScheduled = new DateTime(2015, 6, 6, 23, 59, 59);


        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<UnifonicRestClient>(Credentials.ApplicationSid) { CallBase = true };
        }

        [Test]
        public void ShouldSendMessageWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendSmsMessageResult());
            var client = _mockClient.Object;

            client.SendSmsMessage(OneRecipient, Body, senderId: SenderId, type: SendSmsType.Flash, priority: SendPriority.High, timeScheduled: SendTimeScheduled);

            _mockClient.Verify(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/Send", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(6, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(OneRecipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.IsNotNull(bodyParam);
            Assert.AreEqual(Body, bodyParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);

            var type = savedRequest.Parameters.Find(x => x.Name == "Type");
            Assert.IsNotNull(type);
            Assert.AreEqual(SendSmsType, type.Value);

            var priority = savedRequest.Parameters.Find(x => x.Name == "Priority");
            Assert.IsNotNull(priority);
            Assert.AreEqual(SendPriority, priority.Value);

            var timeScheduled = savedRequest.Parameters.Find(x => x.Name == "TimeScheduled");
            Assert.IsNotNull(timeScheduled.Value);
            Assert.AreEqual(SendTimeScheduled.ToString("yyyy-MM-dd HH:mm:ss"), timeScheduled.Value);
        }

        [Test]
        public void ShouldSendMessageWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendSmsMessageResult());
            var client = _mockClient.Object;

            client.SendSmsMessage(OneRecipient, Body);

            _mockClient.Verify(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/Send", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(2, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(OneRecipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.IsNotNull(bodyParam);
            Assert.AreEqual(Body, bodyParam.Value);
        }

        [Test]
        public void ShouldSendBulkSmsMessagesWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendBulkSmsMessagesResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendBulkSmsMessagesResult());
            var client = _mockClient.Object;

            Assert.IsTrue(ManyRecipients.Contains(","));

            client.SendBulkSmsMessages(ManyRecipients, Body, senderId: SenderId, timeScheduled: SendTimeScheduled);

            _mockClient.Verify(orc => orc.Execute<SendBulkSmsMessagesResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/SendBulk", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(4, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(ManyRecipients, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.IsNotNull(bodyParam);
            Assert.AreEqual(Body, bodyParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);

            var timeScheduled = savedRequest.Parameters.Find(x => x.Name == "TimeScheduled");
            Assert.IsNotNull(timeScheduled.Value);
            Assert.AreEqual(SendTimeScheduled.ToString("yyyy-MM-dd HH:mm:ss"), timeScheduled.Value);
        }


        [Test]
        public void ShouldSendBulkSmsMessagesWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendBulkSmsMessagesResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendBulkSmsMessagesResult());
            var client = _mockClient.Object;

            Assert.IsTrue(ManyRecipients.Contains(","));

            client.SendBulkSmsMessages(ManyRecipients, Body);

            _mockClient.Verify(orc => orc.Execute<SendBulkSmsMessagesResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/SendBulk", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(2, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(ManyRecipients, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.IsNotNull(bodyParam);
            Assert.AreEqual(Body, bodyParam.Value);
        }

        [Test]
        public void ShouldGetMessageStatus()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SmsMessageStatusResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new SmsMessageStatusResult());
            var client = _mockClient.Object;

            client.GetSmsMessageStatus(MessageId);

            _mockClient.Verify(orc => orc.Execute<SmsMessageStatusResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/GetMessageIDStatus", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var messageIdParam = savedRequest.Parameters.Find(x => x.Name == "MessageID");
            Assert.IsNotNull(messageIdParam);
            Assert.AreEqual(MessageId, messageIdParam.Value);
        }

        [Test]
        public void ShouldGetMessagesReport()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SmsMessagesReportResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new SmsMessagesReportResult());
            var client = _mockClient.Object;

            DateTime? dateFrom = DateTime.UtcNow;
            DateTime? dateTo = DateTime.UtcNow;
            SmsMessageStatus? status = SmsMessageStatus.Sent;

            client.GetSmsMessagesReport(dateFrom, dateTo, SenderId, status, Dlr, Country);

            _mockClient.Verify(orc => orc.Execute<SmsMessagesReportResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/GetMessagesReport", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(6, savedRequest.Parameters.Count);

            var dateFromParam = savedRequest.Parameters.Find(x => x.Name == "DateFrom");
            Assert.IsNotNull(dateFromParam);
            Assert.AreEqual(dateFrom.Value.ToString("yyyy-MM-dd"), dateFromParam.Value);

            var dateToParam = savedRequest.Parameters.Find(x => x.Name == "DateTo");
            Assert.IsNotNull(dateToParam);
            Assert.AreEqual(dateTo.Value.ToString("yyyy-MM-dd"), dateToParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);

            var statusParam = savedRequest.Parameters.Find(x => x.Name == "Status");
            Assert.IsNotNull(statusParam);
            Assert.AreEqual(status, statusParam.Value);

            var countryParam = savedRequest.Parameters.Find(x => x.Name == "Country");
            Assert.IsNotNull(countryParam);
            Assert.AreEqual(Country, countryParam.Value);

            var dlrParam = savedRequest.Parameters.Find(x => x.Name == "DLR");
            Assert.IsNotNull(dlrParam);
            Assert.AreEqual(Dlr, dlrParam.Value);
        }

        [Test]
        public void ShouldGetMessagesDetails()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SmsMessagesDetailsResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new SmsMessagesDetailsResult());
            var client = _mockClient.Object;

            DateTime? dateFrom = DateTime.UtcNow;
            DateTime? dateTo = DateTime.UtcNow;
            SmsMessageStatus? status = SmsMessageStatus.Sent;

            int? limit = 500;

            client.GetSmsMessagesDetails(MessageId, dateFrom, dateTo, SenderId, status, Dlr, Country, limit);

            _mockClient.Verify(orc => orc.Execute<SmsMessagesDetailsResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/GetMessagesDetails", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(8, savedRequest.Parameters.Count);

            var messageIdParam = savedRequest.Parameters.Find(x => x.Name == "MessageID");
            Assert.IsNotNull(messageIdParam);
            Assert.AreEqual(MessageId, messageIdParam.Value);

            var dateFromParam = savedRequest.Parameters.Find(x => x.Name == "DateFrom");
            Assert.IsNotNull(dateFromParam);
            Assert.AreEqual(dateFrom.Value.ToString("yyyy-MM-dd"), dateFromParam.Value);

            var dateToParam = savedRequest.Parameters.Find(x => x.Name == "DateTo");
            Assert.IsNotNull(dateToParam);
            Assert.AreEqual(dateTo.Value.ToString("yyyy-MM-dd"), dateToParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);

            var statusParam = savedRequest.Parameters.Find(x => x.Name == "Status");
            Assert.IsNotNull(statusParam);
            Assert.AreEqual(status, statusParam.Value);

            var countryParam = savedRequest.Parameters.Find(x => x.Name == "Country");
            Assert.IsNotNull(countryParam);
            Assert.AreEqual(Country, countryParam.Value);

            var limitParam = savedRequest.Parameters.Find(x => x.Name == "Limit");
            Assert.IsNotNull(limitParam);
            Assert.AreEqual(limit.Value, limitParam.Value);

            var dlrParam = savedRequest.Parameters.Find(x => x.Name == "DLR");
            Assert.IsNotNull(dlrParam);
            Assert.AreEqual(Dlr, dlrParam.Value);
        }

        [Test]
        public void ShouldAddMessagesKeywordWithoutOptionalParameters()
        {

            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new bool());
            var client = _mockClient.Object;

            string number = "123456789";
            string keyword = "12";
            KeywordRules rule = KeywordRules.Contains;
            client.MessagesKeyword(number: number, keyword: keyword, rule: rule);

            _mockClient.Verify(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/Keyword", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(3, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.IsNotNull(numberParam);
            Assert.AreEqual(number, numberParam.Value);

            var keywordParam = savedRequest.Parameters.Find(x => x.Name == "Keyword");
            Assert.IsNotNull(keywordParam);
            Assert.AreEqual(keyword, keywordParam.Value);

            var ruleParam = savedRequest.Parameters.Find(x => x.Name == "Rule");
            Assert.IsNotNull(ruleParam);
            Assert.AreEqual(rule, ruleParam.Value);
        }


        [Test]
        public void ShouldAddMessagesKeywordWithOptionalParameters()
        {

            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new bool());
            var client = _mockClient.Object;

            string number = "123456789";
            string keyWord = "12";
            KeywordRules rule = KeywordRules.Contains;
            string message = ".Net Unit Test Message";
            string webhookUrl = "http://www.XYZ.com";
            string messageParameter = "msg";
            string recipientParameter = "rec";
            RequestType requestType = RequestType.Post;
            string resourceNumber = "159753";

            client.MessagesKeyword(number: number, keyword: keyWord, rule: rule, senderId: SenderId, message: message, webhookUrl: webhookUrl,
                messageParameter: messageParameter, recipientParameter: recipientParameter, requestType: requestType, resourceNumber: resourceNumber);

            _mockClient.Verify(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/Keyword", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(10, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.IsNotNull(numberParam);
            Assert.AreEqual(number, numberParam.Value);

            var keyWordParam = savedRequest.Parameters.Find(x => x.Name == "Keyword");
            Assert.IsNotNull(keyWordParam);
            Assert.AreEqual(keyWord, keyWordParam.Value);

            var ruleParam = savedRequest.Parameters.Find(x => x.Name == "Rule");
            Assert.IsNotNull(ruleParam);
            Assert.AreEqual(rule, ruleParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);

            var messageParam = savedRequest.Parameters.Find(x => x.Name == "Message");
            Assert.IsNotNull(messageParam);
            Assert.AreEqual(message, messageParam.Value);

            var webhookUrlParam = savedRequest.Parameters.Find(x => x.Name == "WebhookURL");
            Assert.IsNotNull(webhookUrlParam);
            Assert.AreEqual(webhookUrl, webhookUrlParam.Value);

            var messageParameterParam = savedRequest.Parameters.Find(x => x.Name == "MessageParameter");
            Assert.IsNotNull(messageParameterParam);
            Assert.AreEqual(messageParameter, messageParameterParam.Value);

            var recipientParameterParam = savedRequest.Parameters.Find(x => x.Name == "RecipientParameter");
            Assert.IsNotNull(recipientParameterParam);
            Assert.AreEqual(recipientParameter, recipientParameterParam.Value);

            var requestTypeParam = savedRequest.Parameters.Find(x => x.Name == "RequestType");
            Assert.IsNotNull(requestTypeParam);
            Assert.AreEqual(requestType, requestTypeParam.Value);

            var resourceNumberParam = savedRequest.Parameters.Find(x => x.Name == "ResourceNumber");
            Assert.IsNotNull(resourceNumberParam);
            Assert.AreEqual(resourceNumber, resourceNumberParam.Value);
        }

        [Test]
        public void ShouldGetMessagesInboxWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<MessagesInboxResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new MessagesInboxResult());
            var client = _mockClient.Object;

            string number = "123456789";
            client.MessagesInbox(number: number);

            _mockClient.Verify(orc => orc.Execute<MessagesInboxResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/Inbox", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.IsNotNull(numberParam);
            Assert.AreEqual(number, numberParam.Value);
        }

        [Test]
        public void ShouldGetMessagesInboxWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<MessagesInboxResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new MessagesInboxResult());
            var client = _mockClient.Object;

            string number = "123456789";
            string keyword = "12";
            DateTime? fromDate = new DateTime(2015,1,1);
            DateTime? toDate = new DateTime(2015,12,30);
            client.MessagesInbox(number: number, keyword: keyword, fromDate: fromDate, toDate: toDate);

            _mockClient.Verify(orc => orc.Execute<MessagesInboxResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/Inbox", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(4, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.IsNotNull(numberParam);
            Assert.AreEqual(number, numberParam.Value);

            var keywordParam = savedRequest.Parameters.Find(x => x.Name == "Keyword");
            Assert.IsNotNull(keywordParam);
            Assert.AreEqual(keyword, keywordParam.Value);

            var fromDateParam = savedRequest.Parameters.Find(x => x.Name == "FromDate");
            Assert.IsNotNull(fromDateParam);
            Assert.AreEqual(fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), fromDateParam.Value);

            var toDateParam = savedRequest.Parameters.Find(x => x.Name == "ToDate");
            Assert.IsNotNull(toDateParam);
            Assert.AreEqual(toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), toDateParam.Value);
        }

        [Test]
        public void ShouldGetScheduledMessages()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetScheduledMessagesResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new GetScheduledMessagesResult());
            var client = _mockClient.Object;

            client.GetScheduledMessages(messageId: MessageId);

            _mockClient.Verify(orc => orc.Execute<GetScheduledMessagesResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/GetScheduled", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var messageIdParam = savedRequest.Parameters.Find(x => x.Name == "MessageID");
            Assert.IsNotNull(messageIdParam);
            Assert.AreEqual(MessageId, messageIdParam.Value);
        }

        [Test]
        public void ShouldStopScheduledMessage()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new bool());
            var client = _mockClient.Object;

            client.StopScheduledMessage(messageId: MessageId);

            _mockClient.Verify(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/StopScheduled", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var messageIdParam = savedRequest.Parameters.Find(x => x.Name == "MessageID");
            Assert.IsNotNull(messageIdParam);
            Assert.AreEqual(MessageId, messageIdParam.Value);
        }

        [Test]
        public void ShouldGetMessagesPricingWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<Dictionary<string,object>>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new Dictionary<string, object>());
            var client = _mockClient.Object;

            client.MessagesPricing();
            _mockClient.Verify(orc => orc.Execute<Dictionary<string, object>>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/Pricing", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(0, savedRequest.Parameters.Count);
        }

        [Test]
        public void ShouldGetMessagesPricingWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<Dictionary<string, object>>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new Dictionary<string, object>());
            var client = _mockClient.Object;

            string countryCode = "SA";
            client.MessagesPricing(countryCode: countryCode);
            _mockClient.Verify(orc => orc.Execute<Dictionary<string, object>>(It.IsAny<IRestRequest>()), Times.Once);
            

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/Pricing", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var countryCodeParam = savedRequest.Parameters.Find(x => x.Name == "CountryCode");
            Assert.IsNotNull(countryCodeParam);
            Assert.AreEqual(countryCode, countryCodeParam.Value);
        }


    }
}
