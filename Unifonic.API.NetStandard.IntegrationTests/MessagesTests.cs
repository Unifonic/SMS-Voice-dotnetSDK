using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace Unifonic.API.NetStandard.IntegrationTests
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

        public MessagesTests()
        {
            _mockClient = new Mock<UnifonicRestClient>(Credentials.ApplicationSid) { CallBase = true };
        }

        [Fact]
        public void ShouldSendMessageWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendSmsMessageResult());
            var client = _mockClient.Object;

            client.SendSmsMessage(OneRecipient, Body, senderId: SenderId, type: SendSmsType.Flash, priority: SendPriority.High, timeScheduled: SendTimeScheduled);

            _mockClient.Verify(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/Send", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(6, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(OneRecipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.NotNull(bodyParam);
            Assert.Equal(Body, bodyParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.NotNull(senderIdParam);
            Assert.Equal(SenderId, senderIdParam.Value);

            var type = savedRequest.Parameters.Find(x => x.Name == "Type");
            Assert.NotNull(type);
            Assert.Equal(SendSmsType, type.Value);

            var priority = savedRequest.Parameters.Find(x => x.Name == "Priority");
            Assert.NotNull(priority);
            Assert.Equal(SendPriority, priority.Value);

            var timeScheduled = savedRequest.Parameters.Find(x => x.Name == "TimeScheduled");
            Assert.NotNull(timeScheduled.Value);
            Assert.Equal(SendTimeScheduled.ToString("yyyy-MM-dd HH:mm:ss"), timeScheduled.Value);
        }

        [Fact]
        public void ShouldSendMessageWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendSmsMessageResult());
            var client = _mockClient.Object;

            client.SendSmsMessage(OneRecipient, Body);

            _mockClient.Verify(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/Send", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(2, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(OneRecipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.NotNull(bodyParam);
            Assert.Equal(Body, bodyParam.Value);
        }

        [Fact]
        public void ShouldSendBulkSmsMessagesWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendBulkSmsMessagesResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendBulkSmsMessagesResult());
            var client = _mockClient.Object;

            Assert.Contains(",", ManyRecipients);

            client.SendBulkSmsMessages(ManyRecipients, Body, senderId: SenderId, timeScheduled: SendTimeScheduled);

            _mockClient.Verify(orc => orc.Execute<SendBulkSmsMessagesResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/SendBulk", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(4, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(ManyRecipients, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.NotNull(bodyParam);
            Assert.Equal(Body, bodyParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.NotNull(senderIdParam);
            Assert.Equal(SenderId, senderIdParam.Value);

            var timeScheduled = savedRequest.Parameters.Find(x => x.Name == "TimeScheduled");
            Assert.NotNull(timeScheduled.Value);
            Assert.Equal(SendTimeScheduled.ToString("yyyy-MM-dd HH:mm:ss"), timeScheduled.Value);
        }


        [Fact]
        public void ShouldSendBulkSmsMessagesWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendBulkSmsMessagesResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendBulkSmsMessagesResult());
            var client = _mockClient.Object;

            Assert.Contains(",", ManyRecipients);

            client.SendBulkSmsMessages(ManyRecipients, Body);

            _mockClient.Verify(orc => orc.Execute<SendBulkSmsMessagesResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/SendBulk", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(2, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(ManyRecipients, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.NotNull(bodyParam);
            Assert.Equal(Body, bodyParam.Value);
        }

        [Fact]
        public void ShouldGetMessageStatus()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SmsMessageStatusResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new SmsMessageStatusResult());
            var client = _mockClient.Object;

            client.GetSmsMessageStatus(MessageId);

            _mockClient.Verify(orc => orc.Execute<SmsMessageStatusResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/GetMessageIDStatus", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var messageIdParam = savedRequest.Parameters.Find(x => x.Name == "MessageID");
            Assert.NotNull(messageIdParam);
            Assert.Equal(MessageId, messageIdParam.Value);
        }

        [Fact]
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
            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/GetMessagesReport", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(6, savedRequest.Parameters.Count);

            var dateFromParam = savedRequest.Parameters.Find(x => x.Name == "DateFrom");
            Assert.NotNull(dateFromParam);
            Assert.Equal(dateFrom.Value.ToString("yyyy-MM-dd"), dateFromParam.Value);

            var dateToParam = savedRequest.Parameters.Find(x => x.Name == "DateTo");
            Assert.NotNull(dateToParam);
            Assert.Equal(dateTo.Value.ToString("yyyy-MM-dd"), dateToParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.NotNull(senderIdParam);
            Assert.Equal(SenderId, senderIdParam.Value);

            var statusParam = savedRequest.Parameters.Find(x => x.Name == "Status");
            Assert.NotNull(statusParam);
            Assert.Equal(status, statusParam.Value);

            var countryParam = savedRequest.Parameters.Find(x => x.Name == "Country");
            Assert.NotNull(countryParam);
            Assert.Equal(Country, countryParam.Value);

            var dlrParam = savedRequest.Parameters.Find(x => x.Name == "DLR");
            Assert.NotNull(dlrParam);
            Assert.Equal(Dlr, dlrParam.Value);
        }

        [Fact]
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
            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/GetMessagesDetails", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(8, savedRequest.Parameters.Count);

            var messageIdParam = savedRequest.Parameters.Find(x => x.Name == "MessageID");
            Assert.NotNull(messageIdParam);
            Assert.Equal(MessageId, messageIdParam.Value);

            var dateFromParam = savedRequest.Parameters.Find(x => x.Name == "DateFrom");
            Assert.NotNull(dateFromParam);
            Assert.Equal(dateFrom.Value.ToString("yyyy-MM-dd"), dateFromParam.Value);

            var dateToParam = savedRequest.Parameters.Find(x => x.Name == "DateTo");
            Assert.NotNull(dateToParam);
            Assert.Equal(dateTo.Value.ToString("yyyy-MM-dd"), dateToParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.NotNull(senderIdParam);
            Assert.Equal(SenderId, senderIdParam.Value);

            var statusParam = savedRequest.Parameters.Find(x => x.Name == "Status");
            Assert.NotNull(statusParam);
            Assert.Equal(status, statusParam.Value);

            var countryParam = savedRequest.Parameters.Find(x => x.Name == "Country");
            Assert.NotNull(countryParam);
            Assert.Equal(Country, countryParam.Value);

            var limitParam = savedRequest.Parameters.Find(x => x.Name == "Limit");
            Assert.NotNull(limitParam);
            Assert.Equal(limit.Value, limitParam.Value);

            var dlrParam = savedRequest.Parameters.Find(x => x.Name == "DLR");
            Assert.NotNull(dlrParam);
            Assert.Equal(Dlr, dlrParam.Value);
        }

        [Fact]
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

            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/Keyword", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(3, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.NotNull(numberParam);
            Assert.Equal(number, numberParam.Value);

            var keywordParam = savedRequest.Parameters.Find(x => x.Name == "Keyword");
            Assert.NotNull(keywordParam);
            Assert.Equal(keyword, keywordParam.Value);

            var ruleParam = savedRequest.Parameters.Find(x => x.Name == "Rule");
            Assert.NotNull(ruleParam);
            Assert.Equal(rule, ruleParam.Value);
        }


        [Fact]
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

            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/Keyword", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(10, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.NotNull(numberParam);
            Assert.Equal(number, numberParam.Value);

            var keyWordParam = savedRequest.Parameters.Find(x => x.Name == "Keyword");
            Assert.NotNull(keyWordParam);
            Assert.Equal(keyWord, keyWordParam.Value);

            var ruleParam = savedRequest.Parameters.Find(x => x.Name == "Rule");
            Assert.NotNull(ruleParam);
            Assert.Equal(rule, ruleParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.NotNull(senderIdParam);
            Assert.Equal(SenderId, senderIdParam.Value);

            var messageParam = savedRequest.Parameters.Find(x => x.Name == "Message");
            Assert.NotNull(messageParam);
            Assert.Equal(message, messageParam.Value);

            var webhookUrlParam = savedRequest.Parameters.Find(x => x.Name == "WebhookURL");
            Assert.NotNull(webhookUrlParam);
            Assert.Equal(webhookUrl, webhookUrlParam.Value);

            var messageParameterParam = savedRequest.Parameters.Find(x => x.Name == "MessageParameter");
            Assert.NotNull(messageParameterParam);
            Assert.Equal(messageParameter, messageParameterParam.Value);

            var recipientParameterParam = savedRequest.Parameters.Find(x => x.Name == "RecipientParameter");
            Assert.NotNull(recipientParameterParam);
            Assert.Equal(recipientParameter, recipientParameterParam.Value);

            var requestTypeParam = savedRequest.Parameters.Find(x => x.Name == "RequestType");
            Assert.NotNull(requestTypeParam);
            Assert.Equal(requestType, requestTypeParam.Value);

            var resourceNumberParam = savedRequest.Parameters.Find(x => x.Name == "ResourceNumber");
            Assert.NotNull(resourceNumberParam);
            Assert.Equal(resourceNumber, resourceNumberParam.Value);
        }

        [Fact]
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

            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/Inbox", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.NotNull(numberParam);
            Assert.Equal(number, numberParam.Value);
        }

        [Fact]
        public void ShouldGetMessagesInboxWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<MessagesInboxResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new MessagesInboxResult());
            var client = _mockClient.Object;

            string number = "123456789";
            string keyword = "12";
            DateTime? fromDate = new DateTime(2015, 1, 1);
            DateTime? toDate = new DateTime(2015, 12, 30);
            client.MessagesInbox(number: number, keyword: keyword, fromDate: fromDate, toDate: toDate);

            _mockClient.Verify(orc => orc.Execute<MessagesInboxResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/Inbox", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(4, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.NotNull(numberParam);
            Assert.Equal(number, numberParam.Value);

            var keywordParam = savedRequest.Parameters.Find(x => x.Name == "Keyword");
            Assert.NotNull(keywordParam);
            Assert.Equal(keyword, keywordParam.Value);

            var fromDateParam = savedRequest.Parameters.Find(x => x.Name == "FromDate");
            Assert.NotNull(fromDateParam);
            Assert.Equal(fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), fromDateParam.Value);

            var toDateParam = savedRequest.Parameters.Find(x => x.Name == "ToDate");
            Assert.NotNull(toDateParam);
            Assert.Equal(toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), toDateParam.Value);
        }

        [Fact]
        public void ShouldGetScheduledMessages()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetScheduledMessagesResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new GetScheduledMessagesResult());
            var client = _mockClient.Object;

            client.GetScheduledMessages(messageId: MessageId);

            _mockClient.Verify(orc => orc.Execute<GetScheduledMessagesResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/GetScheduled", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var messageIdParam = savedRequest.Parameters.Find(x => x.Name == "MessageID");
            Assert.NotNull(messageIdParam);
            Assert.Equal(MessageId, messageIdParam.Value);
        }

        [Fact]
        public void ShouldStopScheduledMessage()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new bool());
            var client = _mockClient.Object;

            client.StopScheduledMessage(messageId: MessageId);

            _mockClient.Verify(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/StopScheduled", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var messageIdParam = savedRequest.Parameters.Find(x => x.Name == "MessageID");
            Assert.NotNull(messageIdParam);
            Assert.Equal(MessageId, messageIdParam.Value);
        }

        [Fact]
        public void ShouldGetMessagesPricingWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<Dictionary<string, object>>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new Dictionary<string, object>());
            var client = _mockClient.Object;

            client.MessagesPricing();
            _mockClient.Verify(orc => orc.Execute<Dictionary<string, object>>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/Pricing", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Empty(savedRequest.Parameters);
        }

        [Fact]
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


            Assert.NotNull(savedRequest);
            Assert.Equal("Messages/Pricing", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var countryCodeParam = savedRequest.Parameters.Find(x => x.Name == "CountryCode");
            Assert.NotNull(countryCodeParam);
            Assert.Equal(countryCode, countryCodeParam.Value);
        }


    }
}
