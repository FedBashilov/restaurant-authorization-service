// Copyright (c) Fedor Bashilov. All rights reserved.

namespace CloudStorage.Service.Exceptions
{
    public class UpdateUserFailedException : Exception
    {
        public UpdateUserFailedException()
        {
        }

        public UpdateUserFailedException(string message)
            : base(message)
        {
        }

        public UpdateUserFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
