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
        private readonly IMagazineService _magazineService;
        public EntryPoint(IMagazineService magazineService)
        {
            _magazineService = magazineService;
        }
        public void Run(string[] args)
        {
            var result = GetResultOfSubmitAnswers();
            Console.WriteLine($"Output: {JsonConvert.SerializeObject(result)}");
            Console.ReadLine();
        }
        public PostAnswerResponse GetResultOfSubmitAnswers()
        {
            var categories = _magazineService.GetCategories().Result.Data;
            var subscribers = _magazineService.GetSubscribers().Result.Data;
            PostAnswerResponse result = new PostAnswerResponse();
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
                    var categoriesCount = subscriber.MagazineIds.Join(magazineList, s => s, m => m.Id, (s, m) => new
                    {
                        m.Category
                    }).ToList().Distinct();

                    if (categoriesCount.Count() == categories.Count())
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
