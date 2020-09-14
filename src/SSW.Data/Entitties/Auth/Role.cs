using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Entitties.Auth
{
    public class Role : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
