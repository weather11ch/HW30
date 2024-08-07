using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW30.Models.Responses
{
    internal class DataModel
    {
        public required int id { get; set; }
        public required string email { get; set; }
        public required string first_name { get; set; }
        public required string last_name { get; set; }
        public required string avatar { get; set; }
    }
}
