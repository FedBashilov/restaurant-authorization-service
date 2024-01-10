// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Exceptions
{
    public class EmailNotConfirmedException : Exception
    {
        public EmailNotConfirmedException()
        {
        }

        public EmailNotConfirmedException(string message)
            : base(message)
        {
        }

        public EmailNotConfirmedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
