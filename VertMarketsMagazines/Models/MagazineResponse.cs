using System;
using System.Collections.Generic;
using System.Text;

namespace VertMarketsMagazines.Models
{
    public class MagazineResponse : APIResponse
    {
        public List<Magazine> Data { get; set; }
    }
}
