using System;
using System.Collections.Generic;

namespace Otsdc
{
    /// <summary>
    /// Sender status
    /// </summary>
    public enum SenderStatus
    {
        /// <summary>
        /// Approved
        /// </summary>
        Approved, 
        /// <summary>
        /// Pending
        /// </summary>
        Pending, 
        /// <summary>
        /// Rejected
        /// </summary>
        Rejected
    }

    /// <summary>
    /// Result of GetBalance
    /// </summary>
    public class GetBalanceResult
    {
        /// <summary>
        /// Your current account balance, the balance is expressed either in USD or SAR
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// The currency code used with cost, either USD or SAR
        /// </summary>
        public string CurrencyCode { get; set; }
    }

    /// <summary>
    /// Result of AddSender
    /// </summary>
    public class AddSenderResult
    {
        /// <summary>
        /// Sender ID status "Approved", "Pending" or "Rejected". Where only approved sender IDs can be used to send text messages
        /// </summary>
        public SenderStatus Status { get; set; }
    }

    /// <summary>
    /// Result of GetSenderStatus
    /// </summary>
    public class GetSenderStatusResult : AddSenderResult
    {
        /// <summary>
        /// The date where the sender ID was created
        /// </summary>
        public DateTime DateCreated { get; set; }
    }

    /// <summary>
    /// Result of GetSenderStatus
    /// </summary>
    public class GetSendersResult
    {
        /// <summary>
        /// list of your sender IDs values with related status for each, IsDefault code and creation date
        /// </summary>
        public List<Sender> SenderNames { get; set; }
    }

    /// <summary>
    /// Result of GetAppDefaultSender
    /// </summary>
    public class GetAppDefaultSenderResult
    {
        /// <summary>
        /// A unique ID that identifies a sender name
        /// </summary>
        public string SenderID { get; set; }
    }

    /// <summary>
    /// sender IDs values with related status for each, IsDefault code and creation date
    /// </summary>
    public class Sender : GetSenderStatusResult
    {
        /// <summary>
        /// A unique ID that identifies a sender name
        /// </summary>
        public string SenderID { get; set; }

        // TODO: IsDefault must be updated to return boolean instead of "0" and "1"

        /// <summary>
        /// True if this sender ID is the default,otherwise false
        /// </summary>
        public string IsDefault { get; set; }
    }




}
