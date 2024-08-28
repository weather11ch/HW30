using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW30.Models.Responses
{
    internal class RegisterResponse
    {
        public int id { get; set; }
        public string token { get; set; }
        public string error { get; set; }
    }
}
