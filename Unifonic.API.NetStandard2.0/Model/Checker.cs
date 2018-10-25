namespace Unifonic
{

    public class NumberInsightResult
    {
        public string InsightID { get; set; }

        /// <summary>
        /// Subscriber status, the possible values are "Reachable" , "No available data", "Not supported destination" , "Unreachable" and "Wrong number format"
        /// </summary>
        public string SubscriberStatus { get; set; }

        /// <summary>
        /// Price of a message total units
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Current balance of your account
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Mobile Network Codes
        /// </summary>
        public string MCC { get; set; }
        /// <summary>
        /// Mobile Network Codes
        /// </summary>
        public string MNC { get; set; }


        public string Country  { get; set; }
        public string Network { get; set; }

        /// <summary>
        /// True if the number seems to be using roaming,otherwise false
        /// </summary>
        public bool IsRoaming { get; set; }
        /// <summary>
        /// The country where number is roaming in
        /// </summary>
        public string RoamingCountry { get; set; }
        /// <summary>
        /// The network where number is roaming in
        /// </summary>
        public string RoamingNetwork { get; set; }

        /// <summary>
        /// True if the number seems to be ported,otherwise false
        /// </summary>
        public bool Ported { get; set; }
        /// <summary>
        /// The network which the number is ported to
        /// </summary>
        public string PortedNetwork { get; set; }

    }
}
