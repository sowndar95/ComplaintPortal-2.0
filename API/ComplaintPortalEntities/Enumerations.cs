using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalEntities
{
    public class Enumerations : BaseEntity
    {
        public string? Group { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}
