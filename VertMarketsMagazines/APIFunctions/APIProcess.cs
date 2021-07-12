using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VertMarketsMagazines.Constants;

namespace VertMarketsMagazines.APIFunctions
{
    public class APIProcess
    {
        private readonly string _baseURL;
        public APIProcess(string baseURL)
        {
            _baseURL = baseURL;
        }
        public async Task<HttpResponseMessage> GetAPIResponse(string actionEndPoint)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_baseURL);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(EndPoints.MEDIATYPE));
                    HttpResponseMessage response = await httpClient.GetAsync($"{_baseURL}/{actionEndPoint}");
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HttpResponseMessage> PostAPIRequest(string apiMethod, string content)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            try
            {
                var httpContent = new StringContent(content, Encoding.UTF8, EndPoints.MEDIATYPE);
                using (var httpClient = new HttpClient())
                {
                    httpResponseMessage = await httpClient.PostAsync($"{_baseURL}/{apiMethod}", httpContent);
                    return httpResponseMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
