using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Exceptions
{
    public class SignInException : Exception
    {
        public SignInException(string message) : base(message)
        {

        }
    }
}
