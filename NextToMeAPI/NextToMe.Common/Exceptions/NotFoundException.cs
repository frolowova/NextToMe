using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NextToMe.Common.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message) { }
    }
}
