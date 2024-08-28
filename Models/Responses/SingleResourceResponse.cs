using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW30.Models.Responses
{
    internal class SingleResourceResponse
    {
        public ListResourceDataModel data { get; set; }
        public SupportModel support { get; set; }
    }
}
