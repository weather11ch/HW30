﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW30.Models.Responses
{
    internal class ListResourceDataModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public string color { get; set; }
        public string pantone_value { get; set; }
    }
}
