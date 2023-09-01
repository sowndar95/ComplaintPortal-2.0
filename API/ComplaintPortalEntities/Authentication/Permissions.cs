using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalEntities
{
    [Flags]
    public enum Permissions : long
    {
        None = 0,
        Admin = 1 << 0,
        Manager = 1 << 1,
        Employee = 1 << 2,
        User = 1 << 3,
        All = ~None
    }
}
