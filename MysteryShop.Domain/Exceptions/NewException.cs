using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Domain.Exceptions
{
    public class NewException : Exception
    {
        public NewException() { }

        public NewException(string message) : base(message)
        {

        }
    }
}
