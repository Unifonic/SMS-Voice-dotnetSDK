using System;
using System.Globalization;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using System.Collections.Generic;

namespace Unifonic
{
    public partial class UnifonicRestClient
    {
        /// <summary>
        ///  Send SMS message to only one recipient; you must have sufficient balance and an active package to send to your desired destination
        /// </summary>
        /// <param name="recipient">Destination mobile number, mobile number must be in international format without 00 or + Example: (4452023498)</param>
        /// <param name="body">Message body supports both English and Unicode characters, concatenated messages is supported</param>
        /// <param name="senderId">The SenderID to send from, default SenderID is used unless else stated</param>
        /// <param name="type">Possible Value is "Flash", support only the following destination KSA- STC</param>
        /// <param name="priority">Send high priority messages, possible value is " High " only. High priority sending is available for advanced plans and through balance</param>
        /// <param name="timeScheduled">Schedule send messages,Note that if time is in the past then message will be sent directly</param>
        public virtual SendSmsMessageResult SendSmsMessage(string recipient, string body, string senderId = null, SendSmsType? type = null, SendPriority? priority = null, DateTime? timeScheduled = null)
        {
            Require.Argument("recipient", recipient);
            Require.Argument("body", body);

            var request = new RestRequest(Method.POST) { Resource = "Messages/Send" };

            if (senderId.HasValue()) request.AddParameter("SenderID", senderId);
            if (timeScheduled.HasValue) request.AddParameter("TimeScheduled", timeScheduled.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            if (type.HasValue) request.AddParameter("Type", type);
            if (priority.HasValue) request.AddParameter("Priority", priority);

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
        /// <param name="timeScheduled">Schedule send messages,Note that if time is in the past then message will be sent directly</param>
        public virtual SendBulkSmsMessagesResult SendBulkSmsMessages(string recipient, string body, string senderId = null, DateTime? timeScheduled = null)
        {
            Require.Argument("recipient", recipient);
            Require.Argument("body", body);

            var request = new RestRequest(Method.POST) { Resource = "Messages/SendBulk" };

            if (senderId.HasValue()) request.AddParameter("SenderID", senderId);
            if (timeScheduled.HasValue) request.AddParameter("TimeScheduled", timeScheduled.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));

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

            if (status.HasValue) request.AddParameter("Status", status.Value);
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
            DateTime? dateFrom = null, DateTime? dateTo = null, string senderId = null,
            SmsMessageStatus? status = null, DlrStatus? dlr = null, string country = null, int? limit = null)
        {
            var request = new RestRequest(Method.POST) { Resource = "Messages/GetMessagesDetails" };

            if (messageId.HasValue()) request.AddParameter("MessageID", messageId);
            if (dateFrom.HasValue) request.AddParameter("DateFrom", dateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (dateTo.HasValue) request.AddParameter("DateTo", dateTo.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (senderId.HasValue()) request.AddParameter("SenderID", senderId);

            if (status.HasValue) request.AddParameter("Status", status.Value);
            if (dlr.HasValue) request.AddParameter("DLR", dlr.Value);
            if (country.HasValue()) request.AddParameter("Country", country);
            if (limit.HasValue) request.AddParameter("Limit", limit.Value);

            return Execute<SmsMessagesDetailsResult>(request);
        }

        /// <summary>
        /// Keyword enables you to manage your interactive numbers keywords , create auto replies or set a webhook directly from your API. 
        /// </summary>
        /// <param name="number">The Inbound number to create an inbound message rule for.</param>
        /// <param name="keyword">The received keyword in the inbound number </param>
        /// <param name="rule">The inbound message keyword rule , possible values are : Is, StartsWith, Contains, Any. Only "Is" is available for a shared number</param>
        /// <param name="senderId">Set any Sender ID for your response action</param>
        /// <param name="message">Set an auto reply to send back to the user (i.e: You have been successfully registered )</param>
        /// <param name="webhookUrl">Defines the source that you want to make the callback to, for example "www.google.com"</param>
        /// <param name="messageParameter">Set the parameters that the source takes ,for example https://www.google.com/search?msg=hello then your parameter that you have to set is msg</param>
        /// <param name="recipientParameter">Set the parameters that the source takes ,for example https://www.google.com/search?rec=966666666666 then your parameter that you have to set is rec</param>
        /// <param name="requestType">Defines the HTTP callback methods , it can be either "Post" or "Get"</param>
        /// <param name="resourceNumber">Your inbound number for example 70001</param>
        public virtual void MessagesKeyword(string number, string keyword, KeywordRules rule,
            string senderId = null, string message = null, string webhookUrl = null, string messageParameter = null,
            string recipientParameter = null, RequestType? requestType = null, string resourceNumber = null)
        {
            Require.Argument("Number", number);
            Require.Argument("Keyword", keyword);
            Require.Argument("Rule", rule);

            var request = new RestRequest(Method.POST) { Resource = "Messages/Keyword" };
            request.AddParameter("Number", number);
            request.AddParameter("Keyword", keyword);
            request.AddParameter("Rule", rule);

            if (senderId.HasValue()) request.AddParameter("SenderID", senderId);
            if (message.HasValue()) request.AddParameter("Message", message);
            if (webhookUrl.HasValue()) request.AddParameter("WebhookURL", webhookUrl);
            if (messageParameter.HasValue()) request.AddParameter("MessageParameter", messageParameter);
            if (recipientParameter.HasValue()) request.AddParameter("RecipientParameter", recipientParameter);
            if (requestType.HasValue) request.AddParameter("RequestType", requestType.Value);
            if (resourceNumber.HasValue()) request.AddParameter("ResourceNumber", resourceNumber);

            Execute<bool>(request);
        }

        /// <summary>
        /// Retrieve all incoming messages to your active shared or dedicated numbers.
        /// </summary>
        /// <param name="number">The Inbound number to create an inbound message rule for.</param>
        /// <param name="keyword">The received keyword in the inbound number </param>
        /// <param name="fromDate">The start date</param>
        /// <param name="toDate">The end date</param>
        public virtual MessagesInboxResult MessagesInbox(string number, string keyword = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            Require.Argument("Number", number);

            var request = new RestRequest(Method.POST) { Resource = "Messages/Inbox" };
            request.AddParameter("Number", number);
            if (keyword.HasValue()) request.AddParameter("Keyword", keyword);
            if (fromDate.HasValue) request.AddParameter("FromDate", fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (toDate.HasValue) request.AddParameter("ToDate", toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

            return Execute<MessagesInboxResult>(request);
        }

        private List<MessagesPricingCountry> ParseMessagesPricingResult(Dictionary<string,object> result)
        {
            var countries = new List<MessagesPricingCountry>();
            foreach (var countryKeyValue in result)
            {
                var country = new MessagesPricingCountry()
                {
                    CountryName = countryKeyValue.Key
                };

                if (countryKeyValue.Value == null)
                {
                    countries.Add(country);
                    continue;
                }

                if (!(countryKeyValue.Value is Dictionary<string, object>))
                {
                    continue;
                }
                var operators = (Dictionary<string, object>)countryKeyValue.Value;

                foreach (var operatorKeyValue in operators)
                {
                    var msgPricingOperator = new MessagesPricingOperator()
                    {
                        OperatorName = operatorKeyValue.Key
                    };

                    var operatorDetails = (Dictionary<string, object>)operatorKeyValue.Value;
                    msgPricingOperator.CountryCode = operatorDetails["CountryCode"] != null ? operatorDetails["CountryCode"].ToString() : null;
                    msgPricingOperator.CountryPrefix = operatorDetails["CountryPrefix"] != null ? operatorDetails["CountryPrefix"].ToString() : null;
                    msgPricingOperator.OperatorPrefix = operatorDetails["OperatorPrefix"] != null ? operatorDetails["OperatorPrefix"].ToString() : null;

                    msgPricingOperator.CurrencyCode = operatorDetails["CurrencyCode"] != null ? operatorDetails["CurrencyCode"].ToString() : null;

                    msgPricingOperator.MCC = operatorDetails["MCC"] != null ? operatorDetails["MCC"].ToString() : null;
                    msgPricingOperator.MNC = operatorDetails["MNC"] != null ? operatorDetails["MNC"].ToString() : null;
                    msgPricingOperator.Cost = operatorDetails["Cost"] != null ?
                    decimal.Parse(operatorDetails["Cost"].ToString(), CultureInfo.InvariantCulture) : default(decimal);

                    country.Operators.Add(msgPricingOperator);
                }
                countries.Add(country);
            }

            return countries;
        }

        /// <summary>
        /// Retrieve outbound messaging pricing for a given country
        /// </summary>
        /// <param name="countryCode">Two letters country code, i.e. "SA" for Saudi Arabia</param>
        public virtual List<MessagesPricingCountry> MessagesPricing(string countryCode = null)
        {
            var request = new RestRequest(Method.POST) { Resource = "Messages/Pricing" };
            if (countryCode.HasValue()) request.AddParameter("CountryCode", countryCode);
            var result = Execute<Dictionary<string, object>>(request);
            return ParseMessagesPricingResult(result);
        }

        /// <summary>
        /// Get a summarized report for scheduled sent messages
        /// </summary>
        /// <param name="messageId">A unique ID that identifies a message</param>
        public virtual GetScheduledMessagesResult GetScheduledMessages(string messageId = null)
        {
            var request = new RestRequest(Method.POST) { Resource = "Messages/GetScheduled" };
            if (messageId.HasValue()) request.AddParameter("MessageID", messageId);
            return Execute<GetScheduledMessagesResult>(request);
        }

        /// <summary>
        /// Stop scheduled messages
        /// </summary>
        /// <param name="messageId">A unique ID that identifies a message</param>
        public virtual void StopScheduledMessage(string messageId)
        {
            Require.Argument("messageId", messageId);

            var request = new RestRequest(Method.POST) { Resource = "Messages/StopScheduled" };

            request.AddParameter("MessageID", messageId);
            Execute<bool>(request);
        }
    }
}
