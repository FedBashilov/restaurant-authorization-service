// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Exceptions
{
    public class RefreshTokensFailedException : Exception
    {
        public RefreshTokensFailedException()
        {
        }

        public RefreshTokensFailedException(string message)
            : base(message)
        {
        }

        public RefreshTokensFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
