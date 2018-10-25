using Moq;
using RestSharp;
using System;
using System.Globalization;
using Xunit;

namespace Unifonic.API.NetStandard.IntegrationTests
{
    public class VoiceTests
    {
        private Mock<UnifonicRestClient> _mockClient;

        private const string Recipient = "962788888888";

        public VoiceTests()
        {
            _mockClient = new Mock<UnifonicRestClient>(Credentials.ApplicationSid) { CallBase = true };
        }

        [Fact]
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
            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/Call", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(2, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(Recipient, recipientParam.Value);

            var contentParam = savedRequest.Parameters.Find(x => x.Name == "Content");
            Assert.NotNull(contentParam);
            Assert.Equal(audioFileUrl, contentParam.Value);

        }

        [Fact]
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
            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/Call", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(7, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(Recipient, recipientParam.Value);

            var contentParam = savedRequest.Parameters.Find(x => x.Name == "Content");
            Assert.NotNull(contentParam);
            Assert.Equal(audioFileUrl, contentParam.Value);

            var callTypeParam = savedRequest.Parameters.Find(x => x.Name == "CallType");
            Assert.NotNull(callTypeParam);
            Assert.Equal(callTypeParam.Value, callType.Value);

            var callerIdParam = savedRequest.Parameters.Find(x => x.Name == "CallerID");
            Assert.NotNull(callerIdParam);
            Assert.Equal(callerIdParam.Value, callerId);

            var timeScheduledParam = savedRequest.Parameters.Find(x => x.Name == "TimeScheduled");
            Assert.NotNull(timeScheduledParam);
            Assert.Equal(timeScheduled.Value.ToString("yyyy-MM-dd HH:mm:ss"), timeScheduledParam.Value);

            var delayParam = savedRequest.Parameters.Find(x => x.Name == "Delay");
            Assert.NotNull(delayParam);
            Assert.Equal(delayParam.Value, delay);

            var repeatParam = savedRequest.Parameters.Find(x => x.Name == "Repeat");
            Assert.NotNull(repeatParam);
            Assert.Equal(repeatParam.Value, repeat.Value);

        }

        [Fact]
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
            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/GetCallIDStatus", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var callIdParam = savedRequest.Parameters.Find(x => x.Name == "CallID");
            Assert.NotNull(callIdParam);
            Assert.Equal(callId, callIdParam.Value);
        }

        [Fact]
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
            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/GetCallsDetails", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(5, savedRequest.Parameters.Count);

            var callIdParam = savedRequest.Parameters.Find(x => x.Name == "CallID");
            Assert.NotNull(callIdParam);
            Assert.Equal(callId, callIdParam.Value);

            var dateFromParam = savedRequest.Parameters.Find(x => x.Name == "DateFrom");
            Assert.NotNull(dateFromParam);
            Assert.Equal(dateFrom.Value.ToString("yyyy-MM-dd"), dateFromParam.Value);

            var dateToParam = savedRequest.Parameters.Find(x => x.Name == "DateTo");
            Assert.NotNull(dateToParam);
            Assert.Equal(dateTo.Value.ToString("yyyy-MM-dd"), dateToParam.Value);

            var statusParam = savedRequest.Parameters.Find(x => x.Name == "Status");
            Assert.NotNull(statusParam);
            Assert.Equal(status, statusParam.Value);

            var countryParam = savedRequest.Parameters.Find(x => x.Name == "Country");
            Assert.NotNull(countryParam);
            Assert.Equal(country, countryParam.Value);
        }

        [Fact]
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
            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/TTSCall", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(3, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(Recipient, recipientParam.Value);

            var contentParam = savedRequest.Parameters.Find(x => x.Name == "Content");
            Assert.NotNull(contentParam);
            Assert.Equal(content, contentParam.Value);

            var langParam = savedRequest.Parameters.Find(x => x.Name == "Language");
            Assert.NotNull(langParam);
            Assert.Equal(lang, langParam.Value);
        }

        [Fact]
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


            client.TtsCall(Recipient, content, lang, callType, callerId, timeScheduled, voice, delay, repeat);

            _mockClient.Verify(orc => orc.Execute<TtsCallResult>(It.IsAny<IRestRequest>()), Times.Once);
            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/TTSCall", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(9, savedRequest.Parameters.Count);

            var recipientParam = savedRequest.Parameters.Find(x => x.Name == "Recipient");
            Assert.NotNull(recipientParam);
            Assert.Equal(Recipient, recipientParam.Value);

            var contentParam = savedRequest.Parameters.Find(x => x.Name == "Content");
            Assert.NotNull(contentParam);
            Assert.Equal(content, contentParam.Value);

            var langParam = savedRequest.Parameters.Find(x => x.Name == "Language");
            Assert.NotNull(langParam);
            Assert.Equal(lang, langParam.Value);


            var callTypeParam = savedRequest.Parameters.Find(x => x.Name == "CallType");
            Assert.NotNull(callTypeParam);
            Assert.Equal(callTypeParam.Value, callType.Value);

            var callerIdParam = savedRequest.Parameters.Find(x => x.Name == "CallerID");
            Assert.NotNull(callerIdParam);
            Assert.Equal(callerIdParam.Value, callerId);

            var timeScheduledParam = savedRequest.Parameters.Find(x => x.Name == "TimeScheduled");
            Assert.NotNull(timeScheduledParam);
            Assert.Equal(timeScheduled.Value.ToString("yyyy-MM-dd HH:mm:ss"), timeScheduledParam.Value);

            var voiceParam = savedRequest.Parameters.Find(x => x.Name == "Voice");
            Assert.NotNull(voiceParam);
            Assert.Equal(voiceParam.Value, voice.Value);

            var delayParam = savedRequest.Parameters.Find(x => x.Name == "Delay");
            Assert.NotNull(delayParam);
            Assert.Equal(delayParam.Value, delay);

            var repeatParam = savedRequest.Parameters.Find(x => x.Name == "Repeat");
            Assert.NotNull(repeatParam);
            Assert.Equal(repeatParam.Value, repeat.Value);


        }

        [Fact]
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

            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/Inbox", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.NotNull(numberParam);
            Assert.Equal(number, numberParam.Value);
        }

        [Fact]
        public void ShouldGetVoiceInboxWithOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<VoiceInboxResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new VoiceInboxResult());
            var client = _mockClient.Object;

            string number = "123456789";
            DateTime? fromDate = new DateTime(2015, 1, 1);
            DateTime? toDate = new DateTime(2015, 12, 30);
            client.VoiceInbox(number: number, fromDate: fromDate, toDate: toDate);

            _mockClient.Verify(orc => orc.Execute<VoiceInboxResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/Inbox", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Equal(3, savedRequest.Parameters.Count);

            var numberParam = savedRequest.Parameters.Find(x => x.Name == "Number");
            Assert.NotNull(numberParam);
            Assert.Equal(number, numberParam.Value);

            var fromDateParam = savedRequest.Parameters.Find(x => x.Name == "FromDate");
            Assert.NotNull(fromDateParam);
            Assert.Equal(fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), fromDateParam.Value);

            var toDateParam = savedRequest.Parameters.Find(x => x.Name == "ToDate");
            Assert.NotNull(toDateParam);
            Assert.Equal(toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), toDateParam.Value);
        }

        [Fact]
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

            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/StopScheduled", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var callIdParam = savedRequest.Parameters.Find(x => x.Name == "CallID");
            Assert.NotNull(callIdParam);
            Assert.Equal(callId, callIdParam.Value);
        }

        [Fact]
        public void ShouldGetScheduledCallsWithoutOptionalParameters()
        {
            IRestRequest savedRequest = null;
            _mockClient.Setup(orc => orc.Execute<GetScheduledCallsResult>(It.IsAny<IRestRequest>()))
               .Callback<IRestRequest>(request => savedRequest = request)
               .Returns(new GetScheduledCallsResult());
            var client = _mockClient.Object;

            client.GetScheduledCalls();

            _mockClient.Verify(orc => orc.Execute<GetScheduledCallsResult>(It.IsAny<IRestRequest>()), Times.Once);

            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/GetScheduled", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Empty(savedRequest.Parameters);
        }


        [Fact]
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

            Assert.NotNull(savedRequest);
            Assert.Equal("Voice/GetScheduled", savedRequest.Resource);
            Assert.Equal(Method.POST, savedRequest.Method);
            Assert.Single(savedRequest.Parameters);

            var callIdParam = savedRequest.Parameters.Find(x => x.Name == "CallID");
            Assert.NotNull(callIdParam);
            Assert.Equal(callId, callIdParam.Value);
        }
    }
}
