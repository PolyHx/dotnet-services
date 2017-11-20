using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace PolyHxDotNetServices.Mail.Inputs
{
    public class SendMailInput
    {
        public String From { get; set; }

        public String[] To { get; set; }

        public String Subject { get; set; }

        public String Text { get; set; }

        public String Html { get; set; }

        public String Template { get; set; }

        public Object Variables { get; set; }

        public Dictionary<string, string> ToDictionnary()
        {
            return GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop =>
                {
                    var val = prop.GetValue(this, null);

                    if (val is string)
                        return val as string;

                    return JsonConvert.SerializeObject(val);
                });
        }
    }
}