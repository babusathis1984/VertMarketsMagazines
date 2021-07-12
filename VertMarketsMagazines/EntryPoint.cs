using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VertMarketsMagazines.Interfaces;
using VertMarketsMagazines.Models;

namespace VertMarketsMagazines
{
    public class EntryPoint
    {
        private readonly IMagazine _magazineService;
        public EntryPoint(IConfiguration configuration, IMagazine magazineService)
        {
            _magazineService = magazineService;
        }
        public void Run(String[] args)
        {
            var result = GetProcessTimings();
            Console.WriteLine($"Result: {JsonConvert.SerializeObject(result)}");
            Console.ReadLine();
        }
        public PostSubscriberAnswers GetProcessTimings()
        {
            var categories = _magazineService.GetCategories().Result.Data;
            var subscribers = _magazineService.GetSubscribers().Result.Data;
            PostSubscriberAnswers result = new PostSubscriberAnswers();
            if (categories.Count > 0 && subscribers.Count > 0)
            {
                List<Magazine> magazineList = new List<Magazine>();

                foreach (var category in categories)
                {
                    var magazine = _magazineService.GetMagazines(category).Result.Data;
                    if (magazine != null)
                    {
                        magazineList.AddRange(magazine);
                    }
                }

                List<string> answerRequest = new List<string>();

                foreach (var subscriber in subscribers)
                {
                    var categoryCount = magazineList.Where(r => subscriber.MagazineIds
                    .Contains(Convert.ToInt32(r.Id))).ToList().Distinct().Count();

                    if (categories.Count.Equals(categoryCount))
                    {
                        answerRequest.Add(subscriber.Id);
                    }
                }

                result = _magazineService.SubmitAnswer(answerRequest).Result;
            }
            return result;
        }
    }
}
