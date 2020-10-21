using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NextToMe.Common.Exceptions
{
    public class InternalServerException : BaseException
    {
        public InternalServerException(string message) : base(HttpStatusCode.InternalServerError, message) { }
    }
}
