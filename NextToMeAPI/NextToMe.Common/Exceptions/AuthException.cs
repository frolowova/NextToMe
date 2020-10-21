using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NextToMe.Common.Exceptions
{
    public class AuthException : BaseException
    {
        public AuthException(string message) : base(HttpStatusCode.Unauthorized, message) { }
        public AuthException(string message, string loggedMessage) : base(HttpStatusCode.Unauthorized, message, loggedMessage) { }

        public AuthException(IEnumerable<IdentityError> identityErrors) : base(HttpStatusCode.Unauthorized, string.Join(";", identityErrors.Select(x => x.Description))) { }
    }
}
