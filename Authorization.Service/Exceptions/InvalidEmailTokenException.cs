// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Exceptions
{
    public class InvalidEmailTokenException : Exception
    {
        public InvalidEmailTokenException()
        {
        }

        public InvalidEmailTokenException(string message)
            : base(message)
        {
        }

        public InvalidEmailTokenException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
