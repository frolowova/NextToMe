using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NextToMe.Common.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message) { }
    }
}
