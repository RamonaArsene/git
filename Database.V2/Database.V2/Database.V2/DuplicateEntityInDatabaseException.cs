using System;
using System.Runtime.Serialization;

namespace Database.V2
{
    [Serializable]
    public class DuplicateEntityInDatabaseException : Exception
    {
        private int id;

        public DuplicateEntityInDatabaseException()
        {
        }

        public DuplicateEntityInDatabaseException(string message) : base(message)
        {
        }

        public DuplicateEntityInDatabaseException(int id)
        {
            this.id = id;
        }

        public DuplicateEntityInDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateEntityInDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}