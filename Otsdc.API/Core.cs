using System;
using System.Reflection;
using RestSharp;
using RestSharp.Deserializers;

namespace Otsdc
{
    /// <summary>
    /// Otsdc rest client
    /// </summary>
    public abstract class OtsdcClient
    {
        /// <summary>
        /// Base Url used in call all APIs
        /// </summary>
        public string BaseUrl { get; private set; }

        /// <summary>
        /// A character string that uniquely identifies your app, you will find your AppSid in "Dev Tools" after you login to OTS Digital Platform
        /// </summary>
        protected string AppSid { get; set; }
        /// <summary>
        /// Rest client
        /// </summary>
        protected RestClient Client;

        /// <summary>
        /// Initialize Rest client
        /// </summary>
        /// <param name="appSid"></param>
        /// <param name="baseUrl"></param>
        protected OtsdcClient(string appSid, string baseUrl)
        {
            BaseUrl = baseUrl;
            AppSid = appSid;

            var assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = new AssemblyName(assembly.FullName);
            var version = assemblyName.Version;

            Client = new RestClient
            {
                UserAgent = "otsdc-csharp/" + version + " (.NET " + Environment.Version + ")",
                BaseUrl = new Uri(BaseUrl),
                Timeout = 60000
            };
            Client.AddHandler("text/html", new JsonDeserializer());
            Client.AddDefaultParameter("AppSid", AppSid);
        }


        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The request to execute</param>
        public virtual T Execute<T>(IRestRequest request) where T : new()
        {
            request.OnBeforeDeserialization = resp =>
            {
                if (((int)resp.StatusCode) >= 400)
                {
                    //RestSharp doesn't like data[]
                    resp.Content = resp.Content.Replace(",\"data\":[]", string.Empty);
                }
            };

            var response = Client.Execute<BaseResult<T>>(request);
            if (response.Data != null && !response.Data.Success)
            {
                var otsdcException = new RestException(response.Data.ErrorCode, response.Data.Message);
                throw otsdcException;
            }
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var otsdcException = new ApplicationException(message, response.ErrorException);
                throw otsdcException;
            }
            return response.Data != null ? response.Data.Data : default(T);
        }
    }
    public partial class OtsdcRestClient : OtsdcClient
    {
        /// <summary>
        /// Initializes a new client
        /// </summary>
        /// <param name="appSid">String that uniquely identifies your app, you will find your AppSid in "Dev Tools" after you login to OTS Digital Platform</param>
        public OtsdcRestClient(string appSid)
            : base(appSid, "http://api.otsdc.com/rest/")
        {
        }
    }
}
