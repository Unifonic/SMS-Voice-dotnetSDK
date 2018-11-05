using Moq;
using RestSharp;
using System;
using System.Globalization;
using Xunit;

namespace Unifonic.API.NetStandard.IntegrationTests
{
    public class VerifyTests
    {
        private Mock<UnifonicRestClient> _mockClient;

        private const string Recipient = "962788888888";
        private const string Body = ".Net Unit Test Message";

        public VerifyTests()
        {
            _mockClient = new Mock<UnifonicRestClient>(Credentials.ApplicationSid) { CallBase = true };
        }

        [Fact]
        public void ShouldSendVerificationCodeWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<SendVerificationCodeResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new SendVerificationCodeResult());
            var client = _mockClient.Object;

            client.SendVerificationCode(Recipient, Body);

            _mockClient.Verify(orc => orc.Execute<SendVerificationCodeResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Verify/GetCode", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(2, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(Recipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.NotNull(bodyParam);
            Assert.Equal(Body, bodyParam.Value);
        }


        [Fact]
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
            Assert.NotNull(savedRequest);
            Assert.Equal("Verify/GetCode", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(8, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(Recipient, recipientParam.Value);

            var bodyParam = savedRequest.Parameters.Find(x => x.Name == "Body");
            Assert.NotNull(bodyParam);
            Assert.Equal(Body, bodyParam.Value);

            var securityTypeParam = savedRequest.Parameters.Find(x => x.Name == "SecurityType");
            Assert.NotNull(securityTypeParam);
            Assert.Equal(securityType, securityTypeParam.Value);

            var expiryParam = savedRequest.Parameters.Find(x => x.Name == "Expiry");
            Assert.NotNull(expiryParam);
            Assert.Equal(expiry.Value.ToString("hh\\:mm\\:ss", CultureInfo.InvariantCulture), expiryParam.Value);

            var senderIdParam = savedRequest.Parameters.Find(x => x.Name == "SenderID");
            Assert.NotNull(senderIdParam);
            Assert.Equal(senderId, senderIdParam.Value);

            var channelParam = savedRequest.Parameters.Find(x => x.Name == "Channel");
            Assert.NotNull(channelParam);
            Assert.Equal(channel, channelParam.Value);

            if (channel.HasValue && channel.Value != Channel.TextMessage)
            {
                var languageParam = savedRequest.Parameters.Find(x => x.Name == "Language");
                Assert.NotNull(languageParam);
                Assert.Equal(language, languageParam.Value);

                var ttlParam = savedRequest.Parameters.Find(x => x.Name == "TTL");
                Assert.NotNull(ttlParam);
                Assert.Equal(ttl, ttlParam.Value);
            }
        }

        [Fact]
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
            Assert.NotNull(savedRequest);
            Assert.Equal("Verify/VerifyNumber", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(2, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(Recipient, recipientParam.Value);

            var passCodeParam = savedRequest.Parameters.Find(x => x.Name == "PassCode");
            Assert.NotNull(passCodeParam);
            Assert.Equal(passCode, passCodeParam.Value);

        }

        [Fact]
        public void ShouldGetVerificationDetailsWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetVerificationDetailsResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetVerificationDetailsResult());
            var client = _mockClient.Object;

            client.GetVerificationDetails();

            _mockClient.Verify(orc => orc.Execute<GetVerificationDetailsResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Verify/GetDetails", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Empty(savedRequest.Parameters);
        }


        [Fact]
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
            Assert.NotNull(savedRequest);
            Assert.Equal("Verify/GetDetails", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(4, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.NotNull(numberParam);
            Assert.Equal(number, numberParam.Value);

            var fromDateParam = savedRequest.Parameters.Find(x => x.Name == "FromDate");
            Assert.NotNull(fromDateParam);
            Assert.Equal(fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), fromDateParam.Value);

            var toDateParam = savedRequest.Parameters.Find(x => x.Name == "ToDate");
            Assert.NotNull(toDateParam);
            Assert.Equal(toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), toDateParam.Value);

            var verifyIdParam = savedRequest.Parameters.Find(x => x.Name == "VerifyID");
            Assert.NotNull(verifyIdParam);
            Assert.Equal(verifyId, verifyIdParam.Value);
        }
    }
}
