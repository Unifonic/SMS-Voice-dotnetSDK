using System;
using System.Collections.Generic;
namespace Unifonic
{
    public enum Channel
    {
        TextMessage,Call,Both
    }

    public enum VerificationSecurityType
    {
        /// <summary>
        /// One time password
        /// </summary>
        OTP
    }

    public class SendVerificationCodeResult
    {
        /// <summary>
        /// A unique id that identifies the verification
        /// </summary>
        public string VerifyID { get; set; }
        /// <summary>
        /// A unique ID that identifies a message
        /// </summary>
        public string MessageID { get; set; }
        /// <summary>
        /// Message send status, the possible values are "Queued" , "Sent", "Failed" and "Rejected"
        /// </summary>
        public string Status { get; set; }
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
        /// Destination mobile number, mobile numbers must be in international format without 00 or + Example: (4452023498)
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// Date a message was created in
        /// </summary>
        public DateTime? TimeCreated { get; set; }
    }

    public class VerifyNumberResult
    {
        /// <summary>
        /// Destination mobile number, mobile numbers must be in international format without 00 or + Example: (4452023498)
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// Number verification status ,possible values are Authenticated , Unauthenticated
        /// </summary>
        public string VerifyStatus { get; set; }
    }

    public class VerificationDetails
    {
        public string VerifyID { get; set; }
        public string Recipient { get; set; }
        /// <summary>
        /// Number verification status ,possible values are Authenticated , Unauthenticated
        /// </summary>
        public string VerifyStatus { get; set; }
        public Channel? Channel { get; set; }
        public bool Expired { get; set; }
        public string Reason { get; set; }
        public DateTime? DateCreated { get; set; }
    }
    public class GetVerificationDetailsResult
    {
        public Int64 NumberOfMessages { get; set; }
        public List<VerificationDetails> Verify { get; set; }
    }
}
