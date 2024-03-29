﻿// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Exceptions
{
    public class RegisterFailedException : Exception
    {
        public RegisterFailedException()
        {
        }

        public RegisterFailedException(string message)
            : base(message)
        {
        }

        public RegisterFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
