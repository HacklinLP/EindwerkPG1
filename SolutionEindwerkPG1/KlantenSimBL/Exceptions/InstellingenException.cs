using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Exceptions
{
    public class InstellingenException : Exception
    {
        public InstellingenException()
        {
        }

        public InstellingenException(string? message) : base(message)
        {
        }

        public InstellingenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
