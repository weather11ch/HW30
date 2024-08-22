using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW30.Models.Responses
{
    internal class UserUpdateResponse
    {
        public required string Name { get; set; }
        public required string Job { get; set; }
        public required int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
