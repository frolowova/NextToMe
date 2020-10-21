using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NextToMe.Common.Exceptions
{
    public class BaseException : Exception
    {
        public Exception Exception { get; set; }
        public HttpStatusCode Code { get; set; }
        public string LoggedMessage { get; set; }
        protected BaseException(HttpStatusCode code, string message) : base(message)
        {
            Code = code;
            LoggedMessage = message;
        }

        protected BaseException(HttpStatusCode code, string message, string loggedMessage) : base(message)
        {
            Code = code;
            LoggedMessage = loggedMessage;
        }
    }
}
