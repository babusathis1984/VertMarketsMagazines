using System;
using System.Collections.Generic;
using System.Text;

namespace VertMarketsMagazines.Models
{
    public class SubscriberData : APIResponse
    {
        public List<Subscriber> Data { get; set; }
    }
}
