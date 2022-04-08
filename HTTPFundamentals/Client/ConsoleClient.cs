using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ConsoleClient
    {
        private HttpClient client = new HttpClient();

        public async Task GetName()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8888/MyName");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }

        public async Task GetSuccess()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8888/Success");
            Console.WriteLine(response.StatusCode);
        }

        public async Task GetRedirection()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8888/Redirection");
            Console.WriteLine(response.StatusCode);
        }

        public async Task GetClientError()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8888/ClientError");
            Console.WriteLine(response.StatusCode);
        }

        public async Task GetServerError()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8888/ServerError");
            Console.WriteLine(response.StatusCode);
        }

        public async Task GetMyNameByHeader()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8888/MyNameByHeader");
            Console.WriteLine(response.Headers.First(header => header.Key == "X-MyName").Value.FirstOrDefault());
        }

        public async Task GetMyNameByCookies()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8888/MyNameByCookies");
            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            string myNameCookie = cookies.FirstOrDefault(cookie => cookie.Split('=')[0] == "MyName");
            Console.WriteLine(myNameCookie.Split('=')[1]);
        }
    }
}
