using System;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Globalization;

namespace Unifonic.API.IntegrationTests
{
    public class VoiceTests
    {
        private Mock<UnifonicRestClient> _mockClient;

        private const string Recipient = "962788888888";


        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<UnifonicRestClient>(Credentials.ApplicationSid) { CallBase = true };
        }

        [Test]
        public void ShouldCallWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<CallResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new CallResult());

            var client = _mockClient.Object;

            var audioFileUrl = new Uri("https://voiceusa.s3.amazonaws.com/voiceWavFiles1423399184883.wav");

            client.Call(Recipient, audioFileUrl);

            _mockClient.Verify(orc => orc.Execute<CallResult>(It.IsAny<IRestRequest>()), Times.Once);
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
        public void ShouldCallWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<CallResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new CallResult());

            var client = _mockClient.Object;

            CallType? callType = CallType.Pull;
            string callerId = "123";
            DateTime? timeScheduled = DateTime.Now.AddDays(2);
            int delay = 1;
            Repeat? repeat = Repeat.Off;

            var audioFileUrl = new Uri("https://voiceusa.s3.amazonaws.com/voiceWavFiles1423399184883.wav");

            client.Call(Recipient, audioFileUrl, callType, callerId, timeScheduled, delay, repeat);

            _mockClient.Verify(orc => orc.Execute<CallResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/Call", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(7, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(Recipient, recipientParam.Value);

            var contentParam = savedRequest.Parameters.Find(x => x.Name == "Content");
            Assert.IsNotNull(contentParam);
            Assert.AreEqual(audioFileUrl, contentParam.Value);

            var callTypeParam = savedRequest.Parameters.Find(x => x.Name == "CallType");
            Assert.IsNotNull(callTypeParam);
            Assert.AreEqual(callTypeParam.Value, callType.Value);

            var callerIdParam = savedRequest.Parameters.Find(x => x.Name == "CallerID");
            Assert.IsNotNull(callerIdParam);
            Assert.AreEqual(callerIdParam.Value, callerId);

            var timeScheduledParam = savedRequest.Parameters.Find(x => x.Name == "TimeScheduled");
            Assert.IsNotNull(timeScheduledParam);
            Assert.AreEqual(timeScheduled.Value.ToString("yyyy-MM-dd HH:mm:ss"), timeScheduledParam.Value);

            var delayParam = savedRequest.Parameters.Find(x => x.Name == "Delay");
            Assert.IsNotNull(delayParam);
            Assert.AreEqual(delayParam.Value, delay);

            var repeatParam = savedRequest.Parameters.Find(x => x.Name == "Repeat");
            Assert.IsNotNull(repeatParam);
            Assert.AreEqual(repeatParam.Value, repeat.Value);

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

            client.GetCallsDetails(callId, dateFrom, dateTo, status, country);

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
        public void ShouldTtsCallWithoutOptionalParameters()
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
            Assert.AreEqual(lang, langParam.Value);
        }

        [Test]
        public void ShouldTtsCallWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<TtsCallResult>(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(request => savedRequest = request)
                .Returns(new TtsCallResult());

            const string content = ".Net Text to Speech Call Unit Test";
            const TtsCallLanguages lang = TtsCallLanguages.English;

            var client = _mockClient.Object;

            CallType? callType = CallType.Pull;
            string callerId = "123";
            DateTime? timeScheduled = DateTime.Now.AddDays(2);
            Voice? voice = Voice.Male;
            int delay = 1;
            Repeat? repeat = Repeat.Off;


            client.TtsCall(Recipient, content, lang,callType,callerId,timeScheduled,voice,delay,repeat);

            _mockClient.Verify(orc => orc.Execute<TtsCallResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/TTSCall", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(9, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.IsNotNull(recipientParam);
            Assert.AreEqual(Recipient, recipientParam.Value);

            var contentParam = savedRequest.Parameters.Find(x => x.Name == "Content");
            Assert.IsNotNull(contentParam);
            Assert.AreEqual(content, contentParam.Value);

            var langParam = savedRequest.Parameters.Find(x => x.Name == "Language");
            Assert.IsNotNull(langParam);
            Assert.AreEqual(lang, langParam.Value);


            var callTypeParam = savedRequest.Parameters.Find(x => x.Name == "CallType");
            Assert.IsNotNull(callTypeParam);
            Assert.AreEqual(callTypeParam.Value, callType.Value);

            var callerIdParam = savedRequest.Parameters.Find(x => x.Name == "CallerID");
            Assert.IsNotNull(callerIdParam);
            Assert.AreEqual(callerIdParam.Value, callerId);

            var timeScheduledParam = savedRequest.Parameters.Find(x => x.Name == "TimeScheduled");
            Assert.IsNotNull(timeScheduledParam);
            Assert.AreEqual(timeScheduled.Value.ToString("yyyy-MM-dd HH:mm:ss"), timeScheduledParam.Value);

            var voiceParam = savedRequest.Parameters.Find(x => x.Name == "Voice");
            Assert.IsNotNull(voiceParam);
            Assert.AreEqual(voiceParam.Value, voice.Value);

            var delayParam = savedRequest.Parameters.Find(x => x.Name == "Delay");
            Assert.IsNotNull(delayParam);
            Assert.AreEqual(delayParam.Value, delay);

            var repeatParam = savedRequest.Parameters.Find(x => x.Name == "Repeat");
            Assert.IsNotNull(repeatParam);
            Assert.AreEqual(repeatParam.Value, repeat.Value);


        }

        [Test]
        public void ShouldGetVoiceInboxWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<VoiceInboxResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new VoiceInboxResult());
            var client = _mockClient.Object;

            string number = "123456789";
            client.VoiceInbox(number: number);

            _mockClient.Verify(orc => orc.Execute<VoiceInboxResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/Inbox", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.IsNotNull(numberParam);
            Assert.AreEqual(number, numberParam.Value);
        }

        [Test]
        public void ShouldGetVoiceInboxWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<VoiceInboxResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new VoiceInboxResult());
            var client = _mockClient.Object;

            string number = "123456789";
            DateTime? fromDate = new DateTime(2015,1,1);
            DateTime? toDate = new DateTime(2015, 12, 30);
            client.VoiceInbox(number: number,fromDate: fromDate,toDate:toDate);

            _mockClient.Verify(orc => orc.Execute<VoiceInboxResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/Inbox", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(3, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.IsNotNull(numberParam);
            Assert.AreEqual(number, numberParam.Value);

            var fromDateParam = savedRequest.Parameters.Find(x => x.Name == "FromDate");
            Assert.IsNotNull(fromDateParam);
            Assert.AreEqual(fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), fromDateParam.Value);

            var toDateParam = savedRequest.Parameters.Find(x => x.Name == "ToDate");
            Assert.IsNotNull(toDateParam);
            Assert.AreEqual(toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), toDateParam.Value);
        }

        [Test]
        public void ShouldStopScheduledCalls()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new bool());
            var client = _mockClient.Object;

            string callId = "123456789";
            client.StopScheduledCalls(callId: callId);

            _mockClient.Verify(orc => orc.Execute<bool>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/StopScheduled", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var callIdParam = savedRequest.Parameters.Find(x => x.Name == "CallID");
            Assert.IsNotNull(callIdParam);
            Assert.AreEqual(callId, callIdParam.Value);
        }

        [Test]
        public void ShouldGetScheduledCallsWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetScheduledCallsResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new GetScheduledCallsResult());
            var client = _mockClient.Object;

            client.GetScheduledCalls();

            _mockClient.Verify(orc => orc.Execute<GetScheduledCallsResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/GetScheduled", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(0, savedRequest.Parameters.Count);
        }


        [Test]
        public void ShouldGetScheduledCallsWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetScheduledCallsResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new GetScheduledCallsResult());
            var client = _mockClient.Object;

            string callId = "123456789";
            client.GetScheduledCalls(callId: callId);

            _mockClient.Verify(orc => orc.Execute<GetScheduledCallsResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("Voice/GetScheduled", savedRequest.Resource);
            Assert.AreEqual(Method.POST, savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);

            var callIdParam = savedRequest.Parameters.Find(x => x.Name == "CallID");
            Assert.IsNotNull(callIdParam);
            Assert.AreEqual(callId, callIdParam.Value);
        }
    }
}
