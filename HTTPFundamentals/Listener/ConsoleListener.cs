using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Listener
{
    public class ConsoleListener
    {
        private HttpListener listener;

        private string name;

        public ConsoleListener(string name, params string[] prefixes)
        {
            this.listener = new HttpListener();
            foreach (string prefix in prefixes)
            {
                this.listener.Prefixes.Add(prefix);
            }

            this.name = name;
        }

        public async Task Listen()
        {
            this.listener.Start();
            Console.WriteLine("Ожидание подключений...");

            HttpListenerContext context = await listener.GetContextAsync();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;            

            string method = request.Url.Segments[1];

            switch (method)
            {
                case "MyName":
                    this.GetMyName(response);
                    break;
                case "Information":
                    this.ResponseInformationStatus(response);
                    break;
                case "Success":
                    this.ResponseSuccessStatus(response);
                    break;
                case "Redirection":
                    this.ResponseRedirectionStatus(response);
                    break;
                case "ClientError":
                    this.ResponseClientErrorStatus(response);
                    break;
                case "ServerError":
                    this.ResponseServerErrorStatus(response);
                    break;
                case "MyNameByHeader":
                    this.GetMyNameByHeader(response);
                    break;
                case "MyNameByCookies":
                    this.MyNameByCookies(response);
                    break;
                default:
                    break;
            }

            this.listener.Stop();
        }

        private void GetMyName(HttpListenerResponse response)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(this.name);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        //problems here
        private void ResponseInformationStatus(HttpListenerResponse response)
        {
            string information = "information";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(information);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            response.StatusCode = (int)HttpStatusCode.EarlyHints;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        private void ResponseSuccessStatus(HttpListenerResponse response)
        {
            this.ResponseStatus(HttpStatusCode.OK, response);
        }

        private void ResponseRedirectionStatus(HttpListenerResponse response)
        {
            this.ResponseStatus(HttpStatusCode.Redirect, response);
        }

        private void ResponseClientErrorStatus(HttpListenerResponse response)
        {
            this.ResponseStatus(HttpStatusCode.BadRequest, response);
        }

        private void ResponseServerErrorStatus(HttpListenerResponse response)
        {
            this.ResponseStatus(HttpStatusCode.InternalServerError, response);
        }

        private void ResponseStatus(HttpStatusCode httpStatusCode, HttpListenerResponse response)
        {
            response.ContentLength64 = 0;
            Stream output = response.OutputStream;
            response.StatusCode = (int)httpStatusCode;
            output.Write(new byte[0], 0, 0);
            output.Close();
        }

        private void GetMyNameByHeader(HttpListenerResponse response)
        {
            response.ContentLength64 = 0;
            Stream output = response.OutputStream;
            response.AddHeader("X-MyName", this.name);
            output.Write(new byte[0], 0, 0);
            output.Close();
        }

        private void MyNameByCookies(HttpListenerResponse response)
        {
            response.ContentLength64 = 0;
            Stream output = response.OutputStream;
            response.AppendCookie(new Cookie("MyName", this.name));
            output.Write(new byte[0], 0, 0);
            output.Close();
        }
    }
}
