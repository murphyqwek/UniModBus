using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniModBus.Exceptions
{
    public class CouldnotReadDataException : Exception
    {
        public CouldnotReadDataException() { }

        public CouldnotReadDataException(string message)
        : base(message)
        {
        }

        public CouldnotReadDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
