using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WindowsFormsApp.Repository.Context
{
    public class ApiContext : IDisposable
    {
        private readonly string _url;
        private readonly HttpClient _client = new HttpClient();

        public ApiContext()
        {
            //Variável armazenada no app.config
            _url = ConfigurationManager.AppSettings["Url"];
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<HttpResponseMessage> Get(string endpoint, string token="")
        {
            
            DefinirToken(token);

            return _client.GetAsync(_url + endpoint);
        }

        public Task<HttpResponseMessage> Post(string endpoint, object data, string token = "")
        {
            if (!string.IsNullOrEmpty(token))
                DefinirToken(token);

            return _client.PostAsJsonAsync(_url + endpoint, data);
        }

        /// <summary>
        /// Definie token em todas as requisições
        /// </summary>
        /// <param name="token">Token gerado na api</param>
        private void DefinirToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _client.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}
