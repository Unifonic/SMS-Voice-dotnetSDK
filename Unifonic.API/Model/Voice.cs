using System;
using System.Collections.Generic;

namespace Unifonic
{
    public enum CallDirection
    {
        Inbound,
        Outbound
    }

    public enum CallFlowAction
    {
        Transfer
    }

    public enum CallType
    {
        Push,
        /// <summary>
        /// pull value used to collect user responses
        /// </summary>
        Pull
    }

    public enum Repeat
    {
        On,Off
    }

    public enum Voice
    {
        Male,Female
    }

    /// <summary>
    /// Supported languages of Text to speech
    /// </summary>
    public enum TtsCallLanguages
    {
        /// <summary>
        /// Arabic
        /// </summary>
        Arabic,
        /// <summary>
        /// English
        /// </summary>
        English
    }

    /// <summary>
    /// Voice call status
    /// </summary>
    public enum VoiceCallStatus
    {
        Queued, 
        Completed, 
        Terminated, 
        Busy, 
        NoAnswer, 
        Rejected,
        Failed,
        Scheduled,
        Stopped,
        Sent
    }

    /// <summary>
    /// Result of making a Call
    /// </summary>
    public class CallResult
    {
        /// <summary>
        /// A unique ID that identifies a voice call
        /// </summary>
        public string CallID { get; set; }

        /// <summary>
        /// Created call status, the possible values are : 
        /// (Queued, Completed , Terminated, Busy,NoAnswer , Rejected and Failed)
        /// </summary>
        public VoiceCallStatus? CallStatus { get; set; }
        /// <summary>
        /// Answered call real duration in seconds
        /// </summary>
        public int? CallDuration { get; set; }
        /// <summary>
        /// Price of a voice call total answered minutes
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Current balance of your account
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Destination mobile number, mobile numbers must be in international format without 00 or + Example: (4452023498)
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// The Date that the voice call has been created 
        /// </summary>
        public DateTime? DateCreated { get; set; }

        //TODO: why CurrencyCode not returned?

        public DateTime? TimeScheduled { get; set; }
    }

    /// <summary>
    /// Result of GetCallStatus
    /// </summary>
    public class GetCallStatusResult
    {
        /// <summary>
        /// Created call status, the possible values are : 
        /// (Queued, Completed , Terminated, Busy, NoAnswer , Rejected and Failed)
        /// </summary>
        public VoiceCallStatus? CallStatus { get; set; }
        /// <summary>
        /// Answered call real duration in seconds
        /// </summary>
        public int? CallDuration { get; set; }
        /// <summary>
        /// Price of a voice call total answered minutes 
        /// TODO: We should unify names,either stick with Price or use Cost,prefer Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Destination mobile number, mobile numbers must be in international format without 00 or + Example: (4452023498)
        /// TODO: why it's not returned in results?
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// The Date that the voice call has been created 
        ///  TODO: We should unify names,either stick with DateCreated or use TimeCreated,prefer DateCreated
        /// </summary>
        public DateTime? DateCreated { get; set; }
        /// <summary>
        /// The Date that the voice call has been picked up
        ///  TODO: We should unify names,either stick with DateStarted or use StartTime,prefer StartTime
        /// </summary>
        public DateTime? DateStarted { get; set; }
        /// <summary>
        /// The Date that the voice call has been hanged up
        /// TODO: We should unify names,either stick with DateEnded or use EndTime,prefer EndTime
        /// </summary>
        public DateTime? DateEnded { get; set; }

        /// <summary>
        /// Recipient response replied using the phone keypad
        /// </summary>
        public string Response { get; set; }
    
    }

    /// <summary>
    /// Result of GetCallsDetails
    /// </summary>
    public class GetCallsDetailsResult
    {
        /// <summary>
        /// List of Calls
        /// </summary>
        public List<Call> Calls { get; set; }
        /// <summary>
        /// The currency code used with cost, either USD or SAR
        /// </summary>
        public string CurrencyCode { get; set; }
        /// <summary>
        /// The total number of a massages of the voice call
        /// </summary>
        public int TotalVoiceMessages { get; set; }
        /// <summary>
        /// The page to display
        /// </summary>
        public int Page { get; set; }
    }

    /// <summary>
    /// Call
    /// </summary>
    public class Call
    {
        /// <summary>
        /// A unique ID that identifies a voice call
        /// </summary>
        public string CallID { get; set; }

        /// <summary>
        /// The URL Link of the audio file, Example : https://voiceusa.s3.amazonaws.com/voiceWavFiles1423399184883.wav
        /// </summary>
        public string AudioURL { get; set; }

        /// <summary>
        /// Destination mobile number, mobile numbers must be in international format without 00 or + Example: (4452023498)
        /// TODO: why RecipientNumber not Recipient? , Rename it to Recipient
        /// </summary>
        public string RecipientNumber { get; set; }

        /// <summary>
        /// The country that the voice call has sent to
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Created call status, the possible values are : 
        /// (Queued, Completed , Terminated, Busy,NoAnswer , Rejected and Failed)
        /// </summary>
        public VoiceCallStatus? CallStatus { get; set; }

        /// <summary>
        /// The Date that the voice call has been created 
        ///  TODO: We should unify names,either stick with DateCreated or use TimeCreated,prefer DateCreated
        /// </summary>
        public DateTime? TimeCreated { get; set; }

        /// <summary>
        /// TODO: We should unify names,either stick with TimeSent or use StartTime,prefer StartTime
        /// //TODO: How it's different from DateStarted/TimeStarted?
        /// </summary>
        public DateTime? TimeSent { get; set; }

        /// <summary>
        /// //TODO: how it's different from TimeSent? both have same value
        /// </summary>
        public DateTime? TimeAnswered { get; set; }

        /// <summary>
        /// The Date that the voice call has been hanged up 
        /// </summary>
        public DateTime? TimeEnded { get; set; }
        /// <summary>
        /// Answered call real duration in seconds
        /// </summary>
        public int? CallDuration { get; set; }

        /// <summary>
        /// Price of a voice call total answered minutes
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Recipient response replied using the phone keypad
        /// </summary>
        public string Response { get; set; }

    }

    /// <summary>
    /// Result of TtsCall
    /// </summary>
    public class TtsCallResult
    {
        /// <summary>
        /// A unique ID that identifies a voice call
        /// </summary>
        public string CallID { get; set; }
        /// <summary>
        /// Created call status, the possible values are : 
        /// (Queued, Completed , Terminated, Busy,NoAnswer , Rejected and Failed)
        /// </summary>
        public VoiceCallStatus? CallStatus { get; set; }

        /// <summary>
        /// Answered call real duration in seconds
        /// </summary>
        public int? CallDuration { get; set; }
        /// <summary>
        /// Cost of a voice call depending on total answered minutes
        /// It was previously Price
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Current balance of your account
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Destination mobile number, mobile numbers must be in international format without 00 or + Example: (4452023498)
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// The Date that the voice call has been created 
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Date when message is scheduled
        /// </summary>
        public DateTime? TimeScheduled { get; set; }

    }


    public class VoiceInboxResult
    {
        public List<VoiceInbox> TotalCalls { get; set; }
    }

    public class VoiceInbox
    {
        public string CallID { get; set; }
        public string Recipient { get; set; }
        public string AudioResponse { get; set; }
        public string Response { get; set; }
        public DateTime DateReceived { get; set; }
        public string CallDuration { get; set; }
        public decimal Cost { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class ScheduledCall
    {
        /// <summary>
        /// A unique ID that identifies a voice call
        /// </summary>
        public string CallID { get; set; }
        /// <summary>
        /// The URL Link of the audio file or text to speech
        /// </summary>
        public string Content { get; set; }
        public string Recipient { get; set; }
        public DateTime? TimeScheduled { get; set; }
        public VoiceCallStatus? Status { get; set; }
    }

    public class GetScheduledCallsResult
    {
        public List<ScheduledCall> Messages { get; set; }
        public string TotalVoiceCalls { get; set; }
        public int Page { get; set; }
    }
}
