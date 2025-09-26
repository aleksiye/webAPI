using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; } 
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
