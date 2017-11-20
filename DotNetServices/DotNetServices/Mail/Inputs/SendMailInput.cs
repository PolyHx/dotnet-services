using System;
using Newtonsoft.Json;

namespace PolyHxDotNetServices.Mail.Inputs
{
    public class SendMailInput
    {
        [JsonProperty("from")]
        public String From { get; set; }

        [JsonProperty("to")]
        public String[] To { get; set; }

        [JsonProperty("subject")]
        public String Subject { get; set; }

        [JsonProperty("text")]
        public String Text { get; set; }

        [JsonProperty("html")]
        public String Html { get; set; }

        [JsonProperty("template")]
        public String Template { get; set; }

        [JsonProperty("variables")]
        public Object Variables { get; set; }

        public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}