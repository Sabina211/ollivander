using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException() : base("Ошибка авторизации") { }
    }
}
