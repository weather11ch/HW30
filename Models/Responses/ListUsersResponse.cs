using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW30.Models.Responses
{
    internal class ListUsersResponse
    {
        public required int page { get; set; }
        public required int per_page { get; set; }
        public required int total { get; set; }
        public required int total_pages { get; set; }
        public required List<DataModel> data { get; set; }          
        public required SupportModel support { get; set; }
    }

}
