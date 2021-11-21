using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Infrastructure.Exceptions
{
    public class NoResultsException : System.Exception
    {
        public NoResultsException(string message) : base(message)
        {

        }
    }
}
