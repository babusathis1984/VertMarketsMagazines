using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VertMarketsMagazines.Constants;
using VertMarketsMagazines.Interfaces;
using VertMarketsMagazines.Models;

namespace VertMarketsMagazines.APIFunctions
{
    public class MagazineStore : IMagazine
    {
        private readonly IConfiguration _configuration;
        private readonly string baseURL;
        private readonly APIProcess apiProcess;
        public MagazineStore(IConfiguration configuration)
        {
            _configuration = configuration;
            baseURL = _configuration["AppSettings:VertBaseURL"];
            apiProcess = new APIProcess(baseURL);
        }
        public async Task<APIResponse> GetToken()
        {
            APIResponse response = new APIResponse();
            try
            {
                HttpResponseMessage responseMessage = await apiProcess.GetAPIResponse(EndPoints.TOKENAPI);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = readTask.GetAwaiter().GetResult();
                    response = JsonConvert.DeserializeObject<APIResponse>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        public async Task<CategoriesResponse> GetCategories()
        {
            CategoriesResponse categories = new CategoriesResponse();
            try
            {
                var token = GetToken().Result.Token;
                HttpResponseMessage responseMessage = await apiProcess.GetAPIResponse($"{EndPoints.CATEGORYAPI}/{token}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = readTask.GetAwaiter().GetResult();
                    categories = JsonConvert.DeserializeObject<CategoriesResponse>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categories;
        }
        public async Task<SubscriberData> GetSubscribers()
        {
            SubscriberData subscribers = new SubscriberData();
            try
            {
                var token = GetToken().Result.Token;
                HttpResponseMessage responseMessage = await apiProcess.GetAPIResponse($"{EndPoints.SUBSCRIBERAPI}/{token}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = readTask.GetAwaiter().GetResult();
                    subscribers = JsonConvert.DeserializeObject<SubscriberData>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return subscribers;
        }
        public async Task<MagazineResponse> GetMagazines(string category)
        {
            MagazineResponse magazines = new MagazineResponse();
            try
            {
                var token = GetToken().Result.Token;
                HttpResponseMessage responseMessage = await apiProcess.GetAPIResponse($"{EndPoints.MAGAZINEAPI}/{token}/{category}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = readTask.GetAwaiter().GetResult();
                    magazines = JsonConvert.DeserializeObject<MagazineResponse>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return magazines;
        }
        public async Task<PostSubscriberAnswers> SubmitAnswer(IEnumerable<string> subscribers)
        {
            PostSubscriberAnswers answersResponse = new PostSubscriberAnswers();
            string result = string.Empty;
            try
            {
                string content = JsonConvert.SerializeObject(subscribers);
                var token = GetToken().Result.Token;
                HttpResponseMessage responseMessage = await apiProcess.PostAPIRequest($"{EndPoints.ANSWERAPI}/{token}", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result = readTask.GetAwaiter().GetResult();
                    answersResponse = JsonConvert.DeserializeObject<PostSubscriberAnswers>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return answersResponse;
        }
    }
}
