using System;
using System.Collections.Generic;

namespace Unifonic
{
    public enum SendPriority
    {
        High
    }

    public enum SendSmsType
    {
        Flash
    }

    /// <summary>
    /// The inbound message keyword rules, only "Is" is available for a shared number
    /// </summary>
    public enum KeywordRules
    {
        Is,
        StartsWith,
        Contains,
        Any
    }

    /// <summary>
    /// HTTP method 
    /// </summary>
    public enum RequestType { Post, Get }

    /// <summary>
    /// Sms message status
    /// </summary>
    public enum SmsMessageStatus
    {
        /// <summary>
        /// Sent
        /// </summary>
        Sent,
        /// <summary>
        /// Queued
        /// </summary>
        Queued,
        /// <summary>
        /// Rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// Failed , available for advanced plans only
        /// </summary>
        Failed,
        /// <summary>
        /// Scheduled
        /// </summary>
        Scheduled
    }

    /// <summary>
    /// Message delivery status returned by networks, the possible values are "Delivered" 
    /// or "Undeliverable", and are available for advanced plans
    /// </summary>
    public enum DlrStatus
    {
        /// <summary>
        /// Delivered
        /// </summary>
        Delivered,
        /// <summary>
        /// Undeliverable
        /// </summary>
        Undeliverable
    }

    /// <summary>
    /// Sms Message
    /// </summary>
    public class BaseSmsMessage
    {
        /// <summary>
        /// A unique ID that identifies a message
        /// </summary>
        public string MessageID { get; set; }
        /// <summary>
        /// Destination mobile number, mobile numbers must be in international format without 00 or + Example: (4452023498)
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// Message send status
        /// </summary>
        public SmsMessageStatus? Status { get; set; }

    }

    /// <summary>
    /// Result of SendSMSMessage
    /// </summary>
    public class SendSmsMessageResult : BaseSmsMessage
    {
        /// <summary>
        /// Number of unit in a message
        /// </summary>
        public int NumberOfUnits { get; set; }
        /// <summary>
        /// Price of a message total units
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// The currency code used with cost, either USD or SAR
        /// </summary>
        public string CurrencyCode { get; set; }
        /// <summary>
        /// Current balance of your account
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Date a message was created in
        /// </summary>
        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// Date when message is scheduled
        /// </summary>
        public DateTime? TimeScheduled { get; set; }
    }

    /// <summary>
    /// Result of SendBulkSmsMessages
    /// </summary>
    public class SendBulkSmsMessagesResult
    {
        /// <summary>
        /// List of sms messages
        /// </summary>
        public List<BaseSmsMessage> Messages { get; set; }
        /// <summary>
        /// Number of unit in a message
        /// </summary>
        public int NumberOfUnits { get; set; }
        /// <summary>
        /// Price of a message total units
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Current balance of your account
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// The currency code used with cost, either USD or SAR
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Date a message was created in
        /// </summary>
        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// Date when message is scheduled
        /// </summary>
        public DateTime? TimeScheduled { get; set; }
    }

    /// <summary>
    /// Result of SmsMessageStatus
    /// </summary>
    public class SmsMessageStatusResult
    {


        /// <summary>
        /// Message send status, the possible values are "Queued" , "Sent", "Failed" and "Rejected"
        /// </summary>
        public SmsMessageStatus? Status { get; set; }

        /// <summary>
        /// Message delivery status returned by networks, the possible values are "Delivered" or "Undeliverable", 
        /// and are available for advanced plans
        /// </summary>
        public string DLR { get; set; }
    }

    /// <summary>
    /// Result of  SmsMessagesReport
    /// </summary>
    public class SmsMessagesReportResult
    {
        /// <summary>
        /// Total number of returned messages
        /// </summary>
        public int TotalTextMessages { get; set; }
        /// <summary>
        /// Number of unit in a message
        /// </summary>
        public int NumberOfUnits { get; set; }
        /// <summary>
        /// Total price
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// The currency code used in cost, either USD or SAR
        /// </summary>
        public string CurrencyCode { get; set; }

    }

    /// <summary>
    /// Result of SmsMessagesDetails
    /// </summary>
    public class SmsMessagesDetailsResult
    {
        /// <summary>
        /// List of sms messages
        /// </summary>
        public List<SmsMessageDetails> Messages { get; set; }
        /// <summary>
        /// The currency code used in cost, either USD or SAR
        /// </summary>
        public string CurrencyCode { get; set; }
        /// <summary>
        /// Total number of returned messages
        /// </summary>
        public int TotalTextMessages { get; set; }
        /// <summary>
        /// The page to display
        /// </summary>
        public int Page { get; set; }
    }

    /// <summary>
    /// Sms message details
    /// </summary>
    public class SmsMessageDetails
    {
        /// <summary>
        /// A unique ID that identifies a message
        /// </summary>
        public string MessageID { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string MessageBody { get; set; }

        /// <summary>
        /// Destination mobile number, mobile numbers must be in international format without 00 or + Example: (4452023498)
        /// </summary>
        public string RecipientNumber { get; set; }
        /// <summary>
        /// The mobile number country the message was sent to
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Message send status, the possible values are "Queued" , "Sent", "Failed" and "Rejected"
        /// </summary>
        public SmsMessageStatus? Status { get; set; }

        /// <summary>
        /// Message delivery status returned by networks, the possible values are "Delivered" or "Undeliverable", 
        /// and are available for advanced plans
        /// </summary>
        public string DLR { get; set; }


        public DateTime? DateCreated { set; get; }

        public DateTime? DateSent { get; set; }

        /// <summary>
        /// The sender name to send from
        /// </summary>
        public string SenderID { get; set; }
        /// <summary>
        /// Number of unit in a message
        /// </summary>
        public int NumberOfUnits { get; set; }
        /// <summary>
        /// Total Price
        /// </summary>
        public decimal Cost { get; set; }
    }

    public class MessagesInboxResult
    {
        public int NumberOfMessages { get; set; }
        public List<MessageInbox> Messages { get; set; }

    }

    public class MessageInbox
    {
        public string MessageFrom { get; set; }
        public string Message { get; set; }
        public DateTime DateReceived { get; set; }
    }

    public class MessagesPricingCountry
    {
        public MessagesPricingCountry()
        {
            Operators = new List<MessagesPricingOperator>();
        }
        public string CountryName { get; set; }
        public List<MessagesPricingOperator> Operators {get;set;}
    }

    public class MessagesPricingOperator
    {
        public string OperatorName { get; set; }
        public string CountryCode { get; set; }
        public string CountryPrefix { get; set; }

        public string OperatorPrefix { get; set; }
        public string MCC { get; set; }
        public string MNC { get; set; }
        public decimal Cost { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class ScheduledMessage : BaseSmsMessage
    {
        /// <summary>
        /// The sent message body text
        /// </summary>
        public string MessageBody { get; set; }
        /// <summary>
        /// The related sender ID
        /// </summary>
        public string SenderID { get; set; }
        /// <summary>
        /// Scheduled message time
        /// </summary>
        public DateTime TimeScheduled { get; set; }
    }

    public class GetScheduledMessagesResult 
    {
        public List<ScheduledMessage> Messages { get; set; }
        /// <summary>
        /// Total number of returned messages
        /// </summary>
        public string TotalTextMessages { get; set; }
        /// <summary>
        /// The page to display
        /// </summary>
        public int Page { get; set; }

    }
}
