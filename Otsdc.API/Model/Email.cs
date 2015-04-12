using System;

namespace Otsdc
{
    /// <summary>
    /// Email Status
    /// </summary>
    public enum EmailStatus
    {
        /// <summary>
        /// Queued
        /// </summary>
        Queued , 
        /// <summary>
        /// Sent
        /// </summary>
        Sent ,
        /// <summary>
        /// Failed
        /// </summary>
        Failed 
    }

    /// <summary>
    /// Result of calling SendEmail
    /// </summary>
    public class SendEmailResult
    {
        /// <summary>
        /// A unique ID that identifies an Email
        /// </summary>
        public string EmailID { set; get;}
        /// <summary>
        /// Email send status, the possible values are (Queued , Sent and Failed )
        /// TODO: Rename it to Status
        /// </summary>
        public EmailStatus? EmailStatus { set; get; }
        /// <summary>
        /// Price of an email total units
        ///TODO: We should unify names,either stick with Price or use Cost,prefer Price
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Current balance of your account
        /// </summary>
        public decimal Balance { get; set;}
        /// <summary>
        /// Email addresses that the email was sent to
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// Date the email was created in
        /// TODO: We should unify names,either stick with DateCreated or use TimeCreated,prefer DateCreated
        /// </summary>
        public DateTime? TimeCreated { get; set; }

        //TODO: why CurrencyCode not returned

    }

    /// <summary>
    /// Result of calling GetEmailReport
    /// </summary>
    public class GetEmailsReportResult
    {
        /// <summary>
        /// Total Price
        /// TODO: We should unify names,either stick with Price or use Cost,prefer Price
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Total number of emails
        /// </summary>
        public int TotalEmails { get; set; }
        /// <summary>
        /// The currency code used with cost, either USD or SAR
        /// </summary>
        public string CurrencyCode { get; set; }
    }
}
