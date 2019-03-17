﻿namespace SimplCommerce.Module.EmailSenderSmtp
{
    public class EmailConfig
    {
        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUsername { get; set; }

        public string SmtpPassword { get; set; }

        public string SmtpFrom { get; set; }

        public string SmtpFromDisplayName { get; set; }
    }
}
