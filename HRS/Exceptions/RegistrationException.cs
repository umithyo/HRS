using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Exceptions
{
    public class RegistrationException:Exception
    {
        public RegistrationException(string message):base(message)
        {
            
        }
    }
}
