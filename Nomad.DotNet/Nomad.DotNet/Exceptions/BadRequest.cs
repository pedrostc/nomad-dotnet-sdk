using System;

namespace Nomad.DotNet.Exceptions
{
    public class BadRequest: Exception
    {
        public BadRequest(string message): base(message)
        { }
    }
}
