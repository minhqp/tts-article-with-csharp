using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace myWebApp
{
    public class TextToSpeech
    {
        public string content { get; set; }
        public string websiteId { get; set; }
        public string httpCallback { get; set; }
        public List<Voice> voices { get; set; }
    }

    public class TextToSpeechResponse
    {
        public int status { get; set; }
    }

    public class Voice
    {
        public string id { get; set; }
        public float rate { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();   
        }
        
        static async Task RunAsync()
        {
           var content = "đây là nội dung bài báo ahihi";
           
           var voices = new List<Voice>();
           var voice = new Voice();
           voice.id = "sg_female_thaotrinh_dialog_48k-hsmm";
           voices.Add(voice);
           
           await Synthesis(content, voices);
        }

        static async Task<bool> Synthesis(string content, List<Voice> voices)
        {
            try
            {
                var websiteId = "5e99a4986kj875f937";
                var httpCallback = "https://moc.portal/api/tts-callback";
                var token = "BFasdfas092oifa0faNYpWsK11I";

                TextToSpeech tts = new TextToSpeech
                {
                    content = content,
                    websiteId = websiteId,
                    httpCallback = httpCallback,
                    voices = voices,
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
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
