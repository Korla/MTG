using System.Net;
using System.IO;

namespace Infrastructure
{
    public class HttpService
    {
        public string Get(string url)
        {
            WebRequest wrGETURL = WebRequest.Create(url);
            Stream objStream = wrGETURL.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);

            return objReader.ReadToEnd();
        }
    }
}
