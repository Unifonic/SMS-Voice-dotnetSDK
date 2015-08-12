using System;
using System.Globalization;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;

namespace Otsdc
{
    public partial class OtsdcRestClient
    {
        /// <summary>
        ///  Send SMS message to only one recipient; you must have sufficient balance and an active package to send to your desired destination
        /// </summary>
        /// <param name="recipient">Destination mobile number, mobile number must be in international format without 00 or + Example: (4452023498)</param>
        /// <param name="body">Message body supports both English and Unicode characters, concatenated messages is supported</param>
        /// <param name="senderId">The SenderID to send from, default SenderID is used unless else stated</param>
        public virtual SendSmsMessageResult SendSmsMessage(string recipient, string body, string senderId = null)
        {
            Require.Argument("recipient", recipient);
            Require.Argument("body", body);

            var request = new RestRequest(Method.POST) {Resource = "Messages/Send"};

            if (senderId.HasValue()) request.AddParameter("SenderID", senderId);

            request.AddParameter("Recipient", recipient);
            request.AddParameter("Body", body);
            return Execute<SendSmsMessageResult>(request);
        }

        /// <summary>
        /// Send bulk SMS messages to multi recipients separated by commas, Using SendBulk API requires authorized API Access, to get your authorized access contact us.
        /// </summary>
        /// <param name="recipient">Destination mobile numbers separated by commas, mobile numbers must be in international format without 00 or + Example: (4452023498)</param>
        /// <param name="body">Message body supports both English and Unicode characters, concatenated messages is supported</param>
        /// <param name="senderId">The SenderID to send from, default SenderID is used unless else stated</param>
        public virtual SendBulkSmsMessagesResult SendBulkSmsMessages(string recipient, string body, string senderId = null)
        {
            Require.Argument("recipient", recipient);
            Require.Argument("body", body);

            var request = new RestRequest(Method.POST) { Resource = "Messages/SendBulk" };

            if (senderId.HasValue()) request.AddParameter("SenderID", senderId);

            request.AddParameter("Recipient", recipient);
            request.AddParameter("Body", body);
            return Execute<SendBulkSmsMessagesResult>(request);
        }

        /// <summary>
        /// Get status of an SMS message
        /// </summary>
        /// <param name="messageId">A unique ID that identifies a message</param>
        public virtual SmsMessageStatusResult GetSmsMessageStatus(string messageId)
        {
             Require.Argument("MessageID", messageId);
             var request = new RestRequest(Method.POST) { Resource = "Messages/GetMessageIDStatus" };
             request.AddParameter("MessageID", messageId);
             return Execute<SmsMessageStatusResult>(request);
        }

 
        /// <summary>
        /// Get a summarized report of sent messages
        /// </summary>
        /// <param name="dateFrom">The start date for the report time interval</param>
        /// <param name="dateTo">The end date for the report time interval</param>
        /// <param name="senderId">Filter messages report according to a specific sender ID</param>
        /// <param name="status">Filter messages report according to a specific message status, "Sent", "Queued", "Rejected" or "Failed"</param>
        /// <param name="dlr">Message delivery status returned by networks, the possible values are "Delivered" or "Undeliverable", and are available for advanced plans</param>
        /// <param name="country">Filter messages report according to a specific destination country</param>
        public virtual SmsMessagesReportResult GetSmsMessagesReport(DateTime? dateFrom = null,
            DateTime? dateTo = null, string senderId = null, SmsMessageStatus? status = null, 
            DlrStatus? dlr = null, string country = null)
        {
            var request = new RestRequest(Method.POST) { Resource = "Messages/GetMessagesReport" };
            if (dateFrom.HasValue) request.AddParameter("DateFrom", dateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (dateTo.HasValue) request.AddParameter("DateTo", dateTo.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (senderId.HasValue()) request.AddParameter("SenderID", senderId);

            if (status.HasValue) request.AddParameter("Status",status.Value);
            if (dlr.HasValue) request.AddParameter("DLR", dlr);
            if (country.HasValue()) request.AddParameter("Country", country);

            return Execute<SmsMessagesReportResult>(request);
        }

        /// <summary>
        /// Get the latest 10,000 created messages
        /// </summary>
        /// <param name="messageId">A unique ID that identifies a message</param>
        /// <param name="dateFrom">The start date for the report time interval</param>
        /// <param name="dateTo">The end date for the report time interval</param>
        /// <param name="senderId">Filter messages report according to a specific sender ID</param>
        /// <param name="status">Filter messages report according to a specific message status, "Sent", "Queued", "Rejected" or "Failed"</param>
        /// <param name="dlr">Message delivery status returned by networks, the possible values are "Delivered" or "Undeliverable", and are available for advanced plans</param>
        /// <param name="country">Filter messages report according to a specific destination country</param>
        /// <param name="limit">Number of messages to return in the report, where the limit maximum is 10,000 and messages are sorted by sending date</param>
        public virtual SmsMessagesDetailsResult GetSmsMessagesDetails(string messageId = null,
            DateTime? dateFrom = null, DateTime? dateTo = null,string senderId = null,
            SmsMessageStatus? status = null,DlrStatus? dlr = null, string country = null, int? limit = null)
        {
            var request = new RestRequest(Method.POST) { Resource = "Messages/GetMessagesDetails" };

            if (messageId.HasValue()) request.AddParameter("MessageID", messageId);
            if (dateFrom.HasValue) request.AddParameter("DateFrom", dateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (dateTo.HasValue) request.AddParameter("DateTo", dateTo.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (senderId.HasValue()) request.AddParameter("SenderID", senderId);

            if (status.HasValue) request.AddParameter("Status",status.Value);
            if (dlr.HasValue) request.AddParameter("DLR", dlr.Value);
            if (country.HasValue()) request.AddParameter("Country", country);
            if (limit.HasValue) request.AddParameter("Limit", limit.Value);

            return Execute<SmsMessagesDetailsResult>(request);
        }
    }
}
