// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Exceptions
{
    public class GoogleVerificationFailedException : Exception
    {
        public GoogleVerificationFailedException()
        {
        }

        public GoogleVerificationFailedException(string message)
            : base(message)
        {
        }

        public GoogleVerificationFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
