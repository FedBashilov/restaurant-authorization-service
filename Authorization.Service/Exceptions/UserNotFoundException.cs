// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message)
            : base(message)
        {
        }

        public UserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
