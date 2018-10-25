using System;
using System.Globalization;
using RestSharp;
using RestSharp.Validation;
using RestSharp.Extensions;

namespace Unifonic
{
    public partial class UnifonicRestClient
    {
        /// <summary>
        /// Make a call to recipient
        /// </summary>
        /// <param name="recipient">Destination mobile number, mobile number must be in international format without 00 or + Example: (4452023498)</param>
        /// <param name="content">The URL Link of the audio file,supported formats are WAV and Mp3, Example : https://voiceusa.s3.amazonaws.com/voiceWavFiles1423399184883.wav </param>
        /// <param name="callType">Defines the type of the call , note that pull value is used to collect user responses</param>
        /// <param name="callerId">Sender callerID, this can be any international mobile number</param>
        /// <param name="timeScheduled">Schedule the call</param>
        /// <param name="delay">Add a pause at the beginning of the call value in seconds, values between (0 – 5 seconds )</param>
        /// <param name="repeat">Repeat the voice call , note: this option can be used only if CallType is set to Pull</param>
        public virtual CallResult Call(string recipient, Uri content, CallType? callType = null,
            string callerId = null, DateTime? timeScheduled = null, int? delay = null, Repeat? repeat = null)
        {
            Require.Argument("Recipient", recipient);
            Require.Argument("Content", content);

            var request = new RestRequest(Method.POST) { Resource = "Voice/Call" };

            request.AddParameter("Recipient", recipient);
            request.AddParameter("Content", content);

            if (callType.HasValue) request.AddParameter("CallType", callType);
            if (callerId.HasValue()) request.AddParameter("CallerID", callerId);
            if (timeScheduled.HasValue) request.AddParameter("TimeScheduled", timeScheduled.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            if (delay.HasValue) request.AddParameter("Delay", delay);
            if (repeat.HasValue) request.AddParameter("Repeat", repeat);

            return Execute<CallResult>(request);
        }

        /// <summary>
        /// Get status of a call
        /// </summary>
        /// <param name="callId">A unique ID that identifies a voice call</param>
        public virtual GetCallStatusResult GetCallStatus(string callId)
        {
            Require.Argument("CallID", callId);
            var request = new RestRequest(Method.POST) { Resource = "Voice/GetCallIDStatus" };
            request.AddParameter("CallID", callId);
            return Execute<GetCallStatusResult>(request);
        }

        /// <summary>
        ///  Get the latest 10,000 created calls
        /// </summary>
        /// <param name="callId">A unique ID that identifies a voice call</param>
        /// <param name="dateFrom">The start date for the report time interval</param>
        /// <param name="dateTo">The end date for the report time interval</param>
        /// <param name="status">Call Status , the possible values are : (Queued, Completed , Terminated, Busy,NoAnswer , Rejected and Failed)</param>
        /// <param name="country">Filter messages report according to a specific destination country</param>
        public virtual GetCallsDetailsResult GetCallsDetails(string callId = null, DateTime? dateFrom = null, DateTime? dateTo = null,
            string status = null, string country = null)
        {
            var request = new RestRequest(Method.POST) { Resource = "Voice/GetCallsDetails" };
            if (callId.HasValue()) request.AddParameter("CallID", callId);
            if (dateFrom.HasValue) request.AddParameter("DateFrom", dateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (dateTo.HasValue) request.AddParameter("DateTo", dateTo.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (status.HasValue()) request.AddParameter("Status", status);
            if (country.HasValue()) request.AddParameter("Country", country);

            return Execute<GetCallsDetailsResult>(request);

        }

        /// <summary>
        /// Make a call using Text to speech,a voice file will be generated based on content and played to the recipient
        /// </summary>
        /// <param name="recipient">Destination mobile number, mobile number must be in international format without 00 or + Example: (4452023498)</param>
        /// <param name="content">Text to send</param>
        /// <param name="language">The language of the text Synthesis , the possible values are :( Arabic, English )</param>
        /// <param name="callType">Defines the type of the call , note that pull value is used to collect user responses</param>
        /// <param name="callerId">Sender callerID, this can be any international mobile number</param>
        /// <param name="timeScheduled">Schedule Calls</param>
        /// <param name="voice">Change voice gender</param>
        /// <param name="delay">Add a pause at the beginning of the call value in seconds, values between (0 – 5 seconds )</param>
        /// <param name="repeat">Repeat the voice call , note: this option can be used only if CallType is set to Pull</param>
        public virtual TtsCallResult TtsCall(string recipient, string content, TtsCallLanguages language,CallType? callType = null,
            string callerId = null, DateTime? timeScheduled = null,Voice? voice=null,int? delay = null,Repeat? repeat = null)
        {
            Require.Argument("Recipient", recipient);
            Require.Argument("Content", content);
            Require.Argument("Language", language);

            var request = new RestRequest(Method.POST) { Resource = "Voice/TTSCall" };
            request.AddParameter("Recipient", recipient);
            request.AddParameter("Content", content);
            request.AddParameter("Language", language);

            if (callType.HasValue) request.AddParameter("CallType",callType);
            if (callerId.HasValue()) request.AddParameter("CallerID", callerId);
            if (timeScheduled.HasValue) request.AddParameter("TimeScheduled", timeScheduled.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            if (voice.HasValue) request.AddParameter("Voice", voice);
            if (delay.HasValue) request.AddParameter("Delay", delay);
            if (repeat.HasValue) request.AddParameter("Repeat", repeat);

            return Execute<TtsCallResult>(request);
        }


        public virtual VoiceInboxResult VoiceInbox(string number, DateTime? fromDate = null, DateTime? toDate = null)
        {
            Require.Argument("Number", number);

            var request = new RestRequest(Method.POST) { Resource = "Voice/Inbox" };

            request.AddParameter("Number", number);

            if (fromDate.HasValue) request.AddParameter("FromDate", fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (toDate.HasValue) request.AddParameter("ToDate", toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

            return Execute<VoiceInboxResult>(request);
        }

        ///// <summary>
        ///// TODO: implement this
        ///// Set a call flow and interact with a call responses to transfer a call to another destination number. 
        ///// Transferred call is priced according to the transferred destination price
        ///// </summary>
        ///// <param name="number"></param>
        ///// <param name="callDirection"></param>
        ///// <param name="response"></param>
        ///// <param name="action"></param>
        ///// <param name="destination">The destination to transfer number to </param>
        //public virtual CallFlowResult CallFlow(string number, CallDirection callDirection, string response,CallFlowAction action,string destination)
        //{

        //}

        /// <summary>
        /// Get a summarized report for scheduled calls
        /// </summary>
        /// <param name="callId">A unique ID that identifies a voice call</param>
        public virtual GetScheduledCallsResult GetScheduledCalls(string callId = null)
        {
            var request = new RestRequest(Method.POST) { Resource = "Voice/GetScheduled" };
            if (callId.HasValue()) request.AddParameter("CallID", callId);
            return Execute<GetScheduledCallsResult>(request);
        }

        /// <summary>
        /// Stop scheduled call
        /// </summary>
        /// <param name="callId">A unique ID that identifies a voice call</param>
        public virtual void StopScheduledCalls(string callId)
        {
            Require.Argument("callId", callId);
            var request = new RestRequest(Method.POST) { Resource = "Voice/StopScheduled" };
            request.AddParameter("CallID", callId);
            Execute<bool>(request);
        }
    }
}
