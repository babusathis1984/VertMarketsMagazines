using System;
using System.Collections.Generic;
using System.Text;

namespace VertMarketsMagazines.Models
{
    public class CategoriesResponse : APIResponse
    {
        public List<string> Data { get; set; }
    }
}
