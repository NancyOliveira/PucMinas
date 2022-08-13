using Domain.Constant;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Domain.Exceptions
{
    [Serializable]
    public class DefaultException : Exception
    {
        public virtual LogLevel Level
        {
            get
            {
                return LogLevel.Error;
            }
        }

        public virtual HttpStatusCode StatusCode
        {
            get
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public DefaultException()
            : base(ExceptionExtensionConstant.DEFAULT_EXCEPTION)
        { }

        protected DefaultException(string message)
            : base(message)
        { }

        public DefaultException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public DefaultException(Exception innerException)
            : base(ExceptionExtensionConstant.DEFAULT_EXCEPTION, innerException)
        { }

        protected DefaultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}