﻿using System;
using System.Net;

namespace Eshop.Share.Exceptions
{
    public class UnAuthrizedException : AppException
    {
        public UnAuthrizedException(HttpStatusCode statusCode, string message, Exception exception = null, object additionalData = null) : base(statusCode, message, exception, additionalData)
        {

        }
    }
}
