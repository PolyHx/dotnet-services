using System;

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
    }
}