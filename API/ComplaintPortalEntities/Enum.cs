using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalEntities
{
    public sealed class EnumDictionary
    {
        public static readonly Dictionary<string, string> Department = new Dictionary<string, string>
        {
            { "Development", "Development" },
            { "BPO", "BPO" },
            { "ProjectManagement", "Project Management" },
        };
    }

    public class ComplaintPortalEnumerations
    {
        public const string Department = "Department";
    }

    public enum Department
    {
        Development,
        BPO,
        ProjectManagement
    }
}
