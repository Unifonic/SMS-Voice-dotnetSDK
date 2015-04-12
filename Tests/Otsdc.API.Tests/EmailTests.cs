using System;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace Otsdc.API.IntegrationTests
{
    public class EmailTests
    {
        private Mock<OtsdcRestClient> _mockClient;

        private const string From = "from@domain.com";
        private const string Subject = ".Net Unit Test Email Subject";

        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<OtsdcRestClient>(Credentials.ApplicationSid){CallBase=true};
        }

        [Test]
        public void ShouldSendEmailWithSubject()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendEmailResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendEmailResult());

            var client = _mockClient.Object;

            const string recipient = "recipient@domain.com";
            const string body = ".Net Unit Test Email Body";
            client.SendEmail(From,recipient,body, Subject);

            _mockClient.Verify(orc => orc.Execute<SendEmailResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Email/Send", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(4, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(recipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.IsNotNull(bodyParam);
            Assert.AreEqual(body, bodyParam.Value);

            var fromParam = savedRequest.Parameters.Find(x => x.Name == "From");
            Assert.IsNotNull(fromParam);
            Assert.AreEqual(From, fromParam.Value);

            var subjectParam = savedRequest.Parameters.Find(x => x.Name == "Subject");
            Assert.IsNotNull(subjectParam);
            Assert.AreEqual(Subject, subjectParam.Value);
        }

        [Test]
        public void ShouldSendEmailWithoutSubject()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendEmailResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendEmailResult());

            var client = _mockClient.Object;

            const string recipient = "recipient@domain.com";
            const string body = ".Net Unit Test Email Body";
            client.SendEmail(From, recipient, body);

            _mockClient.Verify(orc => orc.Execute<SendEmailResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Email/Send", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(3, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(recipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.IsNotNull(bodyParam);
            Assert.AreEqual(body, bodyParam.Value);

            var fromParam = savedRequest.Parameters.Find(x => x.Name == "From");
            Assert.IsNotNull(fromParam);
            Assert.AreEqual(From, fromParam.Value);

        }

        [Test]
        public void ShouldGetEmailsReport()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetEmailsReportResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetEmailsReportResult());

            var client = _mockClient.Object;

            EmailStatus? status = EmailStatus.Sent;
            DateTime? dateFrom = DateTime.UtcNow;
            DateTime? dateTo = DateTime.UtcNow;

            client.GetEmailsReport(status,dateFrom,dateTo,From,Subject);

            _mockClient.Verify(orc => orc.Execute<GetEmailsReportResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Email/GetEmailsReport", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(5, savedRequest.Parameters.Count);

            var statusParam = savedRequest.Parameters.Find(x => x.Name == "Status");
            Assert.IsNotNull(statusParam);
            Assert.AreEqual(status, statusParam.Value);

            var dateFromParam = savedRequest.Parameters.Find(x => x.Name == "DateFrom");
            Assert.IsNotNull(dateFromParam);
            Assert.AreEqual(dateFrom.Value.ToString("yyyy-MM-dd"), dateFromParam.Value);

            var dateToParam = savedRequest.Parameters.Find(x => x.Name == "DateTo");
            Assert.IsNotNull(dateToParam);
            Assert.AreEqual(dateTo.Value.ToString("yyyy-MM-dd"), dateToParam.Value);
            

            var fromParam = savedRequest.Parameters.Find(x => x.Name == "From");
            Assert.IsNotNull(fromParam);
            Assert.AreEqual(From, fromParam.Value);

            var subjectParam = savedRequest.Parameters.Find(x => x.Name == "Subject");
            Assert.IsNotNull(subjectParam);
            Assert.AreEqual(Subject, subjectParam.Value);
        }
    }
}
