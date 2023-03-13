// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException()
        {
        }

        public WrongPasswordException(string message)
            : base(message)
        {
        }

        public WrongPasswordException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
