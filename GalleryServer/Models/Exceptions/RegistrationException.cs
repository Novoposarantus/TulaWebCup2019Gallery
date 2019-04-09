using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Exceptions
{
    public class RegistrationException : Exception
    {
        public RegistrationException() : base() { }
        public RegistrationException(string text) : base(text) { }
    }
}
