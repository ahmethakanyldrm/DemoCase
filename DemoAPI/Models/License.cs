using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Models
{
    public class License
    {
        public int id { get; set; }
        public string LicenseKey { get; set; }
        public string ExpirationDate { get; set; }
    }
}
