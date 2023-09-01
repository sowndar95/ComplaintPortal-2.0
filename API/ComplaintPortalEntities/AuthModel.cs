using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalEntities
{
    public class AuthModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;        
        public Guid UserId { get; set; }
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public IList<Menu> Menus { get; set; } = null!;
        public int Permission { get; set; }
    }
}
