using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;

namespace Otsdc
{
    public partial class OtsdcRestClient
    {
        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="from">The Email address to send from</param>
        /// <param name="recipient">Recipients Email addresses separated by commas</param>
        /// <param name="body">Email body supports both English and Unicode characters</param>
        /// <param name="subject">Subject of the email , default subject is used unless stated</param>
        public virtual SendEmailResult SendEmail(string from,string recipient, string body,string subject = null )
        {
            Require.Argument("Recipient", recipient);
            Require.Argument("Body", body);
            Require.Argument("From", from);

            var request = new RestRequest(Method.POST) { Resource = "Email/Send" };

            request.AddParameter("Recipient", recipient);
            request.AddParameter("Body", body);
            request.AddParameter("From", from);

            if (subject.HasValue()) request.AddParameter("Subject", subject);

            return Execute<SendEmailResult>(request);

        }

        /// <summary>
        /// Get a summarized report of sent messages
        /// </summary>
        /// <param name="status">Email send status, the possible values are (Queued , Sent and Failed )</param>
        /// <param name="dateFrom">The start date for the report time interval</param>
        /// <param name="dateTo">The end date for the report time interval</param>
        /// <param name="from">Filter report base on from email address</param>
        /// <param name="subject">Subject of the email , default subject is used unless stated</param>
        public virtual GetEmailsReportResult GetEmailsReport(EmailStatus? status = null, 
            DateTime? dateFrom = null,DateTime? dateTo = null, string from = null, string subject = null)
        {
            var request = new RestRequest(Method.POST) { Resource = "Email/GetEmailsReport" };

            if (status.HasValue) request.AddParameter("Status", status.Value);
            if (dateFrom.HasValue) request.AddParameter("DateFrom", dateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (dateTo.HasValue) request.AddParameter("DateTo", dateTo.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (from.HasValue()) request.AddParameter("From", from);
            if (subject.HasValue()) request.AddParameter("Subject", subject);
            return Execute<GetEmailsReportResult>(request);
        }

    }
}
