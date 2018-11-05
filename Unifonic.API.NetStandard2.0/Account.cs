using System.Collections.Generic;
using RestSharp;
using RestSharp.Validation;

namespace Unifonic
{
    /// <summary>
    /// Unifonic rest client
    /// </summary>
    public partial class UnifonicRestClient
    {
        /// <summary>
        /// Get account balance
        /// </summary>
        public virtual GetBalanceResult GetBalance()
        {
            var request = new RestRequest(Method.POST) { Resource = "Account/GetBalance" };
            return Execute<GetBalanceResult>(request);
        }

        /// <summary>
        /// Add a sender name
        /// </summary>
        /// <param name="senderId">Sender ID should not exceed 11 characters or 16 numbers, only English letters allowed with no special characters or spaces</param>
        public virtual AddSenderResult AddSender(string senderId)
        {
            Require.Argument("SenderID", senderId);
            var request = new RestRequest(Method.POST) { Resource = "Account/AddSenderID" };
            request.AddParameter("SenderID", senderId);
            return Execute<AddSenderResult>(request);
        }

        /// <summary>
        /// Get status of a sender name
        /// </summary>
        /// <param name="senderId">Sender ID should not exceed 11 characters or 16 numbers, only English letters allowed with no special characters or spaces</param>
        public virtual GetSenderStatusResult GetSenderStatus(string senderId)
        {
            Require.Argument("SenderId", senderId);
            var request = new RestRequest(Method.POST) { Resource = "Account/GetSenderIDStatus" };
            request.AddParameter("SenderID", senderId);
            return Execute<GetSenderStatusResult>(request);
        }

        /// <summary>
        /// Get list of your sender IDs values with related status for each, IsDefault code and creation date
        /// </summary>
        public virtual List<Sender> GetSenders()
        {
            var request = new RestRequest(Method.POST) { Resource = "Account/GetSenderIDs" };
            var listSenders= Execute<GetSendersResult>(request);
            return listSenders.SenderNames;
        }

        /// <summary>
        /// Remove a sender name
        /// </summary>
        /// <param name="senderId">Sender ID should not exceed 11 characters or 16 numbers, only English letters allowed with no special characters or spaces</param>
        public virtual void DeleteSender(string senderId)
        {
            Require.Argument("SenderId", senderId);
            var request = new RestRequest(Method.POST) { Resource = "Account/DeleteSenderID" };
            request.AddParameter("SenderID", senderId);
            Execute<bool>(request);
        }

        /// <summary>
        /// Get The default SenderID, to be considered by your application to send text messages
        /// </summary>
        public virtual GetAppDefaultSenderResult GetAppDefaultSender()
        {
            var request = new RestRequest(Method.POST) { Resource = "Account/GetAppDefaultSenderID" };
            return Execute<GetAppDefaultSenderResult>(request);
        }

        /// <summary>
        /// Change the default SenderID
        /// </summary>
        /// <param name="senderId">Sender ID should not exceed 11 characters or 16 numbers, only English letters allowed with no special characters or spaces</param>
        public virtual void ChangeAppDefaultSender(string senderId)
        {
            Require.Argument("SenderID", senderId);
            var request = new RestRequest(Method.POST) { Resource = "Account/ChangeAppDefaultSenderID" };
            request.AddParameter("SenderID", senderId);
            Execute<bool>(request);
        }


    }
}
