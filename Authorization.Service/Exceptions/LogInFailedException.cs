// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Exceptions
{
    public class LogInFailedException : Exception
    {
        public LogInFailedException()
        {
        }

        public LogInFailedException(string message)
            : base(message)
        {
        }

        public LogInFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
