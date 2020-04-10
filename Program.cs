using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;


namespace myWebApp
{
    public class TextToSpeech
    {
        public string content { get; set; }
        public string websiteId { get; set; }
        public string httpCallback { get; set; }
    }

    public class TextToSpeechResponse
    {
        public int status { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();   
        }
        
        static async Task RunAsync()
        {
           await Synthesis();
        }

        static async Task<bool> Synthesis()
        {
            try
            {

                TextToSpeech tts = new TextToSpeech
                {
                    content = "alo",
                    websiteId = "5e69f6856d821625ce3dfasf3",
                    httpCallback = "https://callback.com/webhooks",
                };

                var jsonRequest = JsonConvert.SerializeObject(tts);
                var url = "https://articles.vbee.vn/api/articles";
                var data = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "kP8FVep10h0tLGd7");

                HttpResponseMessage response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;
                var jsonResponse = JsonConvert.DeserializeObject<TextToSpeechResponse>(result);
                Console.WriteLine(jsonResponse.status);
                if (jsonResponse.status == 1) return true;
                return false;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
