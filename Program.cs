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
                var content = "đây là nội dung bài báo";
                var websiteId = "5e69f6856d821625ce37d8d8";
                var httpCallback = "https://callback.com/webhooks";
                var token = "fa902fa0mfa02lf09";

                TextToSpeech tts = new TextToSpeech
                {
                    content = content,
                    websiteId = websiteId,
                    httpCallback = httpCallback,
                };

                var jsonRequest = JsonConvert.SerializeObject(tts);
                var url = "https://articles.vbee.vn/api/articles";
                var data = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", token);

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
