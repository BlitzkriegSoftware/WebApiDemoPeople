using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Demo.People.WebAPI.Library
{
    /// <summary>
    /// Validation Exception
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {
        private readonly string resourceName;
        private readonly IList<string> validationErrors;

        /// <summary>
        /// CTOR
        /// </summary>
        public ValidationException()
        {
        }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="message">Message</param>
        public ValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="innerException">Inner Exception</param>
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="resourceName"></param>
        /// <param name="validationErrors"></param>
        public ValidationException(string message, string resourceName, IList<string> validationErrors)
            : base(message)
        {
            this.resourceName = resourceName;
            this.validationErrors = validationErrors;
        }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="resourceName">The name of the thing being validated</param>
        /// <param name="validationErrors">List of validation errors</param>
        /// <param name="innerException">Inner Exception</param>
        public ValidationException(string message, string resourceName, IList<string> validationErrors, Exception innerException)
            : base(message, innerException)
        {
            this.resourceName = resourceName;
            this.validationErrors = validationErrors;
        }

        // Constructor should be protected for unsealed classes, private for sealed classes.
        // (The Serializer invokes this constructor through reflection, so it can be private)
        /// <summary>
        /// De-Serializer CTOR
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.resourceName = info.GetString("ResourceName");
            this.validationErrors = (IList<string>)info.GetValue("ValidationErrors", typeof(IList<string>));
        }

        /// <summary>
        /// The name of the thing being validated
        /// </summary>
        public string ResourceName
        {
            get { return this.resourceName; }
        }

        /// <summary>
        /// List of validation errors
        /// </summary>
        public IList<string> ValidationErrors
        {
            get { return this.validationErrors; }
        }

        /// <summary>
        /// Serializer
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("ResourceName", this.ResourceName);

            // Note: if "List<T>" isn't serializable you may need to work out another
            //       method of adding your list, this is just for show...
            info.AddValue("ValidationErrors", this.ValidationErrors, typeof(IList<string>));

            // MUST call through to the base class to let it save its own state
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Nicely formatted validation list
        /// </summary>
        /// <param name="separator">Separator</param>
        /// <returns>List as a string</returns>
        public string ValidationText(string separator)
        {
            if (this.ValidationErrors != null)
                return string.Join(separator, this.ValidationErrors.ToArray());
            else
                return string.Empty;
        }

    }

}