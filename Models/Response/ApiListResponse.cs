using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response
{
    public class ApiListResponse<T>
    {
        public bool Success { get; set; }

        public IEnumerable<T>? Data { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
