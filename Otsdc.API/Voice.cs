using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;

namespace Otsdc
{
    public partial class OtsdcRestClient
    {
        /// <summary>
        /// Make a call to recipient
        /// </summary>
        /// <param name="recipient">Destination mobile number, mobile number must be in international format without 00 or + Example: (4452023498)</param>
        /// <param name="content">The URL Link of the audio file,supported formats are WAV and Mp3, Example : https://voiceusa.s3.amazonaws.com/voiceWavFiles1423399184883.wav </param>
        public virtual CallResult Call(string recipient,Uri content)
        {
            Require.Argument("Recipient", recipient);
            Require.Argument("Content", content);

             var request = new RestRequest(Method.POST) {Resource = "Voice/Call"};

            request.AddParameter("Recipient", recipient);
            request.AddParameter("Content", content);
            return Execute<CallResult>(request);
        }

        /// <summary>
        /// Get status of a call
        /// </summary>
        /// <param name="callId">A unique ID that identifies a voice call</param>
        public virtual GetCallStatusResult GetCallStatus(string callId)
        {
            Require.Argument("CallID", callId);
            var request = new RestRequest(Method.POST) {Resource = "Voice/GetCallIDStatus"};
            request.AddParameter("CallID", callId);
            return Execute<GetCallStatusResult>(request);
        }

        /// <summary>
        ///  Get the latest 10,000 created calls
        /// </summary>
        /// <param name="callId">A unique ID that identifies a voice call</param>
        /// <param name="dateFrom">The start date for the report time interval</param>
        /// <param name="dateTo">The end date for the report time interval</param>
        /// <param name="status">Call Status , the possible values are : (Queued, Completed , Terminated, Busy,NoAnswer , Rejected and Failed)
        /// TODO: it would be easier if status is enum</param>
        /// <param name="country">Filter messages report according to a specific destination country</param>
        public virtual GetCallsDetailsResult GetCallsDetails( string callId=null,DateTime? dateFrom = null,DateTime? dateTo=null,
            string status=null,string country=null)
        {
            var request = new RestRequest(Method.POST) {Resource = "Voice/GetCallsDetails"};
            if (callId.HasValue()) request.AddParameter("CallID", callId);
            if (dateFrom.HasValue) request.AddParameter("DateFrom", dateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (dateTo.HasValue) request.AddParameter("DateTo", dateTo.Value.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture));
            if (status.HasValue()) request.AddParameter("Status", status);
            if (country.HasValue()) request.AddParameter("Country", country);

            return Execute<GetCallsDetailsResult>(request);

        }

        /// <summary>
        /// Make a call using Text to speech,a voice file will be generated based on content and played to the recipient
        /// </summary>
        /// <param name="recipient">Destination mobile number, mobile number must be in international format without 00 or + Example: (4452023498)</param>
        /// <param name="content">The URL Link of the audio file, Example : https://voiceusa.s3.amazonaws.com/voiceWavFiles1423399184883.wav</param>
        /// <param name="language">The language of the text Synthesis , the possible values are :( Arabic, English )</param>
        public virtual TtsCallResult TtsCall(string recipient,string content,TtsCallLanguages language)
        {
            Require.Argument("Recipient", recipient);
             Require.Argument("Content", content);
             Require.Argument("Language", language);

             var request = new RestRequest(Method.POST) {Resource = "Voice/TTSCall"};
            request.AddParameter("Recipient", recipient);
            request.AddParameter("Content", content);
            request.AddParameter("Language", language == TtsCallLanguages.English ? "english" : "arabic");

            return Execute<TtsCallResult>(request);
        }


    }
}
