using System;
using System.Globalization;

namespace Otsdc
{
    /// <summary>
    /// Rest exception
    /// </summary>
    public class RestException : Exception
    {
        private string _errorCode;

        /// <summary>
        /// Code of the error
        /// </summary>
        public string ErrorCode
        {
            get { return _errorCode; }
        }

        /// <summary>
        /// Initialize a new Rest exception
        /// </summary>
        /// <param name="errorCode">Code of the error</param>
        /// <param name="message">Error Message</param>
        public RestException(string errorCode,string message):base(message)
        {
            _errorCode=errorCode;
        }

        /// <summary>
        /// Creates and returns a string representation of the current exception
        /// </summary>
        public override string ToString()
        {
            return string.Format( CultureInfo.InvariantCulture,"RestException -> {0} : {1}", ErrorCode, Message);
        }
    }
}
