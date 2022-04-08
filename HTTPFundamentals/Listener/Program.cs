using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Listener
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            ConsoleListener listener = new ConsoleListener("Vova", "http://localhost:8888/");

            while (true)
            {
                await listener.Listen();
            }
        }
    }
}
