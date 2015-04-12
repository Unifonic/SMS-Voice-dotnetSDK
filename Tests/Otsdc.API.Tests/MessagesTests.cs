using System;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace Otsdc.API.IntegrationTests
{
    public class MessagesTests
    {
        private Mock<OtsdcRestClient> _mockClient;

        private const string OneRecipient = "962788888888";
        private const string ManyRecipients = "962788888888,962796666666";
        private const string Body = ".Net Unit Test Message";
        private const string SenderId = "unitTest";
        private const string MessageId = "123";
        private const string Country = "Jordan";
        private DlrStatus? _dlr = DlrStatus.Delivered;


        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<OtsdcRestClient>(Credentials.ApplicationSid) { CallBase = true };
        }

        [Test]
        public void ShouldSendMessageWithoutSenderId()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendSmsMessageResult());
            var client = _mockClient.Object;

            client.SendSmsMessage(OneRecipient,Body);

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
        public void ShouldSendMessageWithSenderId()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendSmsMessageResult());
            var client = _mockClient.Object;

            client.SendSmsMessage(OneRecipient, Body, SenderId);

            _mockClient.Verify(orc => orc.Execute<SendSmsMessageResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/Send", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(3, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(OneRecipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.IsNotNull(bodyParam);
            Assert.AreEqual(Body, bodyParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);
        }

        [Test]
        public void ShouldSendBulkSmsMessagesWithoutSenderId()
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
        public void ShouldSendBulkSmsMessagesWithSenderId()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendBulkSmsMessagesResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendBulkSmsMessagesResult());
            var client = _mockClient.Object;

            Assert.IsTrue(ManyRecipients.Contains(","));

            client.SendBulkSmsMessages(ManyRecipients, Body, SenderId);

            _mockClient.Verify(orc => orc.Execute<SendBulkSmsMessagesResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/SendBulk", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(3, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(ManyRecipients, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.IsNotNull(bodyParam);
            Assert.AreEqual(Body, bodyParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(SenderId, senderIdParam.Value);
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

            client.GetSmsMessagesReport(dateFrom,dateTo, SenderId, status,_dlr,Country);

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
            Assert.AreEqual(_dlr.Value, dlrParam.Value);
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

            client.GetSmsMessagesDetails(MessageId, dateFrom, dateTo, SenderId, status,_dlr,Country, limit);

            _mockClient.Verify(orc => orc.Execute<SmsMessagesDetailsResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Messages/GetMessagesDetails", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(7, savedRequest.Parameters.Count);

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
            Assert.AreEqual(_dlr.Value, dlrParam.Value);
        }
    }
}
