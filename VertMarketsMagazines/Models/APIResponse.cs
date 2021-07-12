using System;
using System.Collections.Generic;
using System.Text;

namespace VertMarketsMagazines.Models
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
