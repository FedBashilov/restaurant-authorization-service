// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Exceptions
{
    public class EmailVerificationFailedException : Exception
    {
        public EmailVerificationFailedException()
        {
        }

        public EmailVerificationFailedException(string message)
            : base(message)
        {
        }

        public EmailVerificationFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
