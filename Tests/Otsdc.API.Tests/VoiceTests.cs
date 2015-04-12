using System;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace Otsdc.API.IntegrationTests
{
    public class VoiceTests
    {
        private Mock<OtsdcRestClient> _mockClient;

        private const string Recipient = "962788888888";
        

        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<OtsdcRestClient>(Credentials.ApplicationSid) {CallBase = true};
        }

        [Test]
        public void ShouldCall()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<CallResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest=request)
                .Returns(new CallResult());

            var client = _mockClient.Object;

            var audioFileUrl = new Uri("https://voiceusa.s3.amazonaws.com/voiceWavFiles1423399184883.wav");

            client.Call(Recipient, audioFileUrl);

            _mockClient.Verify(orc => orc.Execute<CallResult>(It.IsAny<IRestRequest>()),Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/Call", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(2, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(Recipient, recipientParam.Value);

            var contentParam = savedRequest.Parameters.Find(x => x.Name == "Content");
            Assert.IsNotNull(contentParam);
            Assert.AreEqual(audioFileUrl, contentParam.Value);

        }

        [Test]
        public void ShouldGetVoiceCallStatus()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetCallStatusResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetCallStatusResult());

            var client = _mockClient.Object;

            const string callId = "123";

            client.GetCallStatus(callId);

            _mockClient.Verify(orc => orc.Execute<GetCallStatusResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/GetCallIDStatus", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var callIdParam = savedRequest.Parameters.Find(x => x.Name == "CallID");
            Assert.IsNotNull(callIdParam);
            Assert.AreEqual(callId, callIdParam.Value);
        }

        [Test]
        public void ShouldGetCallsDetails()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetCallsDetailsResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new GetCallsDetailsResult());

            var client = _mockClient.Object;

            const string callId = "123";
            DateTime? dateFrom = DateTime.UtcNow;
            DateTime? dateTo = DateTime.UtcNow;
            const string status = "Sent";
            const string country = "Jordan";

            client.GetCallsDetails(callId,dateFrom,dateTo,status,country);

            _mockClient.Verify(orc => orc.Execute<GetCallsDetailsResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/GetCallsDetails", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(5, savedRequest.Parameters.Count);

            var callIdParam = savedRequest.Parameters.Find(x => x.Name == "CallID");
            Assert.IsNotNull(callIdParam);
            Assert.AreEqual(callId, callIdParam.Value);

            var dateFromParam = savedRequest.Parameters.Find(x => x.Name == "DateFrom");
            Assert.IsNotNull(dateFromParam);
            Assert.AreEqual(dateFrom.Value.ToString("yyyy-MM-dd"), dateFromParam.Value);

            var dateToParam = savedRequest.Parameters.Find(x => x.Name == "DateTo");
            Assert.IsNotNull(dateToParam);
            Assert.AreEqual(dateTo.Value.ToString("yyyy-MM-dd"), dateToParam.Value);

            var statusParam = savedRequest.Parameters.Find(x => x.Name == "Status");
            Assert.IsNotNull(statusParam);
            Assert.AreEqual(status, statusParam.Value);

            var countryParam = savedRequest.Parameters.Find(x => x.Name == "Country");
            Assert.IsNotNull(countryParam);
            Assert.AreEqual(country, countryParam.Value);
        }

        [Test]
        public void ShouldTtsCall()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<TtsCallResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new TtsCallResult());

            const string content = ".Net Text to Speech Call Unit Test";
            const TtsCallLanguages lang = TtsCallLanguages.English;

            var client = _mockClient.Object;

            client.TtsCall(Recipient, content, lang);

            _mockClient.Verify(orc => orc.Execute<TtsCallResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/TTSCall", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(3, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(Recipient, recipientParam.Value);

            var contentParam = savedRequest.Parameters.Find(x => x.Name == "Content");
            Assert.IsNotNull(contentParam);
            Assert.AreEqual(content, contentParam.Value);

            var langParam = savedRequest.Parameters.Find(x => x.Name == "Language");
            Assert.IsNotNull(langParam);
            Assert.AreEqual(lang.ToString().ToLower(), langParam.Value);
        }
    }
}
