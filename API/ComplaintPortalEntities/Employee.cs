using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalEntities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string EmployeeID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Aadhar { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string ReportingManager { get; set; } = string.Empty;        
    }
}
