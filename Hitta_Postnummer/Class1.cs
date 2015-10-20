using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hitta_Postnummer
{
    public class Result
    {
        public string zipcode { get; set; }
        public string address { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class RootObject
    {
        public int status_code { get; set; }
        public string status_message { get; set; }
        public string response_charset { get; set; }
        public List<Result> results { get; set; }
    }
}
