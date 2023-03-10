// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Exceptions
{
    public class InvalidRefreshTokenException : Exception
    {
        public InvalidRefreshTokenException()
        {
        }

        public InvalidRefreshTokenException(string message)
            : base(message)
        {
        }

        public InvalidRefreshTokenException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
