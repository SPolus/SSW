using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Entitties.Auth
{
    public class User : Person
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
