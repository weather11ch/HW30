using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW30.Models.Responses
{
    internal class UserResponse
    {
        public required string Name { get; set; }
        public required string Job { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
