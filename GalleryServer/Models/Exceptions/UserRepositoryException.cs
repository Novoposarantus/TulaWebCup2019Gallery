using System;

namespace Models.Exceptions
{
    public class UserRepositoryException : Exception
    {
        public UserRepositoryException() : base() { }
        public UserRepositoryException(string text) : base(text) { }
    }
}
