using System;
using System.Collections.Generic;
using System.Text;

namespace VertMarketsMagazines.Models
{
    public class SubmitAnswerResponse
    {
        public string TotalTime { get; set; }
        public bool AnswerCorrect { get; set; }
        public List<string> ShouldBe { get; set; }
    }
}
