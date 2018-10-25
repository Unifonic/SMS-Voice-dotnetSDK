using RestSharp;
using RestSharp.Validation;
using RestSharp.Extensions;
using System;
using System.Globalization;

namespace Unifonic
{
    public partial class UnifonicRestClient
    {
        /// <summary>
        /// Provide a user number to be verified,it will send pass code via text message or TTS call or both to the provided number
        /// Note that generated pass-code will be concatenated with this body before sending to recipient
        /// </summary>
        /// <param name="recipient">Destination mobile number, mobile numbers must be in international format without 00 or + Example: (4452023498)</param>
        /// <param name="body">Message body,will be used in text message or in TTS call or both</param>
        /// <param name="securityType">The pass code validity usage ,possible values are "OTP" -One Time pass code- that can be used only for one time and "TSP" -Time Session pass code- that can be used many times until it's expired</param>
        /// <param name="expiry">The expiry time of a pass code , after which the pass code will get expired.The default value is 24h , and expiry format is HH:mm:ss</param>
        /// <param name="senderId">The SenderId to send from, App default SenderId is used unless else stated</param>
        /// <param name="channel">The desired communication channel, possible values are "TextMessage", "Call" or "Both"</param>
        /// <param name="language">If the channel was set to "Call", language must be defined to enable the text to speech call</param>
        /// <param name="ttl">Time To Live,time in minutes to define when the TTS call will be sent, default value is 1 minute and maximum value is 15 minutes, 
        /// and should not exceed the expiry time for the code</param>
        public virtual SendVerificationCodeResult SendVerificationCode(string recipient, string body, VerificationSecurityType? securityType = null,
            TimeSpan? expiry = null, string senderId = null, Channel? channel = null, TtsCallLanguages? language = null, int? ttl = null)
        {

            Require.Argument("recipient", recipient);
            Require.Argument("body", body);

            if (channel.HasValue && channel.Value != Channel.TextMessage)
            {
                Require.Argument("language", language);
                Require.Argument("ttl", ttl);
            }

            var request = new RestRequest(Method.POST) { Resource = "Verify/GetCode" };
            request.AddParameter("Recipient", recipient);
            request.AddParameter("Body", body);

            if (securityType.HasValue) request.AddParameter("SecurityType", securityType);
            if (expiry.HasValue) request.AddParameter("Expiry", expiry.Value.ToString("hh\\:mm\\:ss",CultureInfo.InvariantCulture));
            if (senderId.HasValue()) request.AddParameter("SenderID", senderId);
            if (channel.HasValue) request.AddParameter("Channel", channel);
            if (language.HasValue) request.AddParameter("Language", language);
            if (ttl.HasValue) request.AddParameter("TTL", ttl);

            return Execute<SendVerificationCodeResult>(request);
        }

        /// <summary>
        /// Retrieve verification details such as authenticated, in progress, unauthenticated numbers, reasons for unauthenticated and the used channel 
        /// </summary>
        /// <param name="number">TODO:document these</param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="verifyId"></param>
        public virtual GetVerificationDetailsResult GetVerificationDetails(string number = null, DateTime? fromDate = null, DateTime? toDate = null, string verifyId = null)
        {
            var request = new RestRequest(Method.POST) { Resource = "Verify/GetDetails" };
            if (number.HasValue()) request.AddParameter("Number", number);
            if (fromDate.HasValue) request.AddParameter("FromDate", fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (toDate.HasValue) request.AddParameter("ToDate", toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (verifyId.HasValue()) request.AddParameter("VerifyID", verifyId);

            return Execute<GetVerificationDetailsResult>(request);
        }

        /// <summary>
        /// Verify if the pass code matches the number and if the mobile number is authenticated not
        /// </summary>
        /// <param name="recipient">Destination mobile number, mobile numbers must be in international format without 00 or + Example: (4452023498)</param>
        /// <param name="passCode">The code received on recipient mobile</param>
        public virtual VerifyNumberResult VerifyNumber(string recipient,string passCode)
        {
            Require.Argument("recipient", recipient);
            Require.Argument("passCode", passCode);

            var request = new RestRequest(Method.POST) { Resource = "Verify/VerifyNumber" };
            request.AddParameter("Recipient", recipient);
            request.AddParameter("PassCode", passCode);

            return Execute<VerifyNumberResult>(request);
        }
    }
}
