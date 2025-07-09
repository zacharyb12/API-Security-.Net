using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UserModel
{
    public class UpdateUser
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}
