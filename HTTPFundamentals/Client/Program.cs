using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            ConsoleClient client = new ConsoleClient();

            await client.GetName();

            await client.GetSuccess();

            await client.GetRedirection();

            await client.GetClientError();

            await client.GetServerError();

            await client.GetMyNameByHeader();

            await client.GetMyNameByCookies();
        }
    }
}
