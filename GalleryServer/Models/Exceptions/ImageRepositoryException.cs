using System;

namespace Models.Exceptions
{
    public class ImageRepositoryException : Exception
    {
        public ImageRepositoryException() : base() { }
        public ImageRepositoryException(string text) : base(text) { }
    }
}
