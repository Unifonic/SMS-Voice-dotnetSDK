using System;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Globalization;

namespace Unifonic.API.IntegrationTests
{
    class VerifyTests
    {
        private Mock<UnifonicRestClient> _mockClient;

        private const string Recipient = "962788888888";
        private const string Body = ".Net Unit Test Message";

        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<UnifonicRestClient>(Credentials.ApplicationSid) { CallBase = true };
        }

        [Test]
        public void ShouldSendVerificationCodeWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendVerificationCodeResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendVerificationCodeResult());
            var client = _mockClient.Object;

            client.SendVerificationCode(Recipient, Body);

            _mockClient.Verify(orc => orc.Execute<SendVerificationCodeResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Verify/GetCode", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(2, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(Recipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.IsNotNull(bodyParam);
            Assert.AreEqual(Body, bodyParam.Value);
        }


        [Test]
        public void ShouldSendVerificationCodeWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendVerificationCodeResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendVerificationCodeResult());
            var client = _mockClient.Object;

            VerificationSecurityType? securityType = VerificationSecurityType.OTP;
            TimeSpan? expiry = new TimeSpan(0, 20, 00);
            string senderId = "unitTest";
            Channel? channel = Channel.Call;
            TtsCallLanguages? language = TtsCallLanguages.English;
            int? ttl = 5;

            client.SendVerificationCode(Recipient, Body, securityType: securityType, expiry: expiry, senderId: senderId,
                channel: channel, language: language, ttl: ttl);

            _mockClient.Verify(orc => orc.Execute<SendVerificationCodeResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Verify/GetCode", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(8, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(Recipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.IsNotNull(bodyParam);
            Assert.AreEqual(Body, bodyParam.Value);

            var securityTypeParam = savedRequest.Parameters.Find(x => x.Name == "SecurityType");
            Assert.IsNotNull(securityTypeParam);
            Assert.AreEqual(securityType, securityTypeParam.Value);

            var expiryParam = savedRequest.Parameters.Find(x => x.Name == "Expiry");
            Assert.IsNotNull(expiryParam);
            Assert.AreEqual(expiry.Value.ToString("hh\\:mm\\:ss", CultureInfo.InvariantCulture), expiryParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.IsNotNull(senderIdParam);
            Assert.AreEqual(senderId, senderIdParam.Value);

            var channelParam = savedRequest.Parameters.Find(x => x.Name == "Channel");
            Assert.IsNotNull(channelParam);
            Assert.AreEqual(channel, channelParam.Value);

            if (channel.HasValue && channel.Value != Channel.TextMessage)
            {
                var languageParam = savedRequest.Parameters.Find(x => x.Name == "Language");
                Assert.IsNotNull(languageParam);
                Assert.AreEqual(language, languageParam.Value);

                var ttlParam = savedRequest.Parameters.Find(x => x.Name == "TTL");
                Assert.IsNotNull(ttlParam);
                Assert.AreEqual(ttl, ttlParam.Value);
            }
        }

        [Test]
        public void ShouldVerifyNumber()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<VerifyNumberResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new VerifyNumberResult());
            var client = _mockClient.Object;

            string passCode = "1234";
            client.VerifyNumber(Recipient, passCode);

            _mockClient.Verify(orc => orc.Execute<VerifyNumberResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Verify/VerifyNumber", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(2, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(Recipient, recipientParam.Value);

            var passCodeParam = savedRequest.Parameters.Find(x => x.Name == "PassCode");
            Assert.IsNotNull(passCodeParam);
            Assert.AreEqual(passCode, passCodeParam.Value);

        }

        [Test]
        public void ShouldGetVerificationDetailsWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetVerificationDetailsResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetVerificationDetailsResult());
            var client = _mockClient.Object;

            client.GetVerificationDetails();

            _mockClient.Verify(orc => orc.Execute<GetVerificationDetailsResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Verify/GetDetails", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(0, savedRequest.Parameters.Count);
        }


        [Test]
        public void ShouldGetVerificationDetailsWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetVerificationDetailsResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetVerificationDetailsResult());
            var client = _mockClient.Object;


            string number = "123456789";
            DateTime? fromDate = DateTime.Now.AddMonths(-1);
            DateTime? toDate = DateTime.Now;
            string verifyId = "1452369";

            client.GetVerificationDetails(number: number, fromDate: fromDate, toDate: toDate, verifyId: verifyId);

            _mockClient.Verify(orc => orc.Execute<GetVerificationDetailsResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Verify/GetDetails", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(4, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.IsNotNull(numberParam);
            Assert.AreEqual(number, numberParam.Value);

            var fromDateParam = savedRequest.Parameters.Find(x => x.Name == "FromDate");
            Assert.IsNotNull(fromDateParam);
            Assert.AreEqual(fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), fromDateParam.Value);

            var toDateParam = savedRequest.Parameters.Find(x => x.Name == "ToDate");
            Assert.IsNotNull(toDateParam);
            Assert.AreEqual(toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), toDateParam.Value);

            var verifyIdParam = savedRequest.Parameters.Find(x => x.Name == "VerifyID");
            Assert.IsNotNull(verifyIdParam);
            Assert.AreEqual(verifyId, verifyIdParam.Value);
        }
    }
}
