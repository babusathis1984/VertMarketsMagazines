using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VertMarketsMagazines.Models;

namespace VertMarketsMagazines.Interfaces
{
    public interface IMagazineService
    {
        Task<CategoriesResponse> GetCategories();       
        Task<SubscriberData> GetSubscribers();      
        Task<MagazineResponse> GetMagazines(string category);     
        Task<PostAnswerResponse> SubmitAnswer(IEnumerable<string> subcribers);
        Task<APIResponse> GetToken();
    }
}
