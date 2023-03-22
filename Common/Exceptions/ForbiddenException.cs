using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("Доступ запрещен") { }
    }
}
