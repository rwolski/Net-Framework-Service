using System;

namespace Framework.API
{
    public class IncorrectAttributeException : ArgumentNullException
    {
        readonly string _message;

        public IncorrectAttributeException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get { return _message; }
        }
    }
}
