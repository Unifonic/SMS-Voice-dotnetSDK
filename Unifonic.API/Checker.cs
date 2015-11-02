using RestSharp;
using RestSharp.Validation;

namespace Unifonic
{
    public partial class UnifonicRestClient
    {
        /// <summary>
        /// Check the reachability of the number
        /// </summary>
        /// <param name="recipient">Destination mobile number, mobile number must be in international format without 00 or + Example: (4452023498)</param>
        public virtual NumberInsightResult NumberInsight(string recipient)
        {
            Require.Argument("recipient", recipient);

            var request = new RestRequest(Method.POST) { Resource = "Checker/NumberInsight" };

           request.AddParameter("Recipient", recipient);
           return Execute<NumberInsightResult>(request);
        }
    }
}
