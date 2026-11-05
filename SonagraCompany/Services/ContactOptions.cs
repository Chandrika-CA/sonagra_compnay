namespace SonagraCompany.Services
{
    /// <summary>
    /// Settings for enquiry notifications ("Contact" section of appsettings / user-secrets).
    /// Secrets (SMTP password, Twilio token) must NOT be committed - keep them in user-secrets or environment variables.
    /// </summary>
    public class ContactOptions
    {
        public const string SectionName = "Contact";

        /// <summary>Where the enquiry e-mail is delivered.</summary>
        public string NotifyEmail { get; set; } = string.Empty;

        /// <summary>10-digit Indian mobile number that receives the WhatsApp and SMS alerts.</summary>
        public string NotifyPhone { get; set; } = string.Empty;

        public SmtpOptions Smtp { get; set; } = new();
        public TwilioOptions Twilio { get; set; } = new();
        public Fast2SmsOptions Fast2Sms { get; set; } = new();
    }

    public class SmtpOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        /// <summary>Sender address; defaults to User when empty.</summary>
        public string From { get; set; } = string.Empty;
        public string FromName { get; set; } = "Sonagra & Company";
    }

    /// <summary>Indian SMS gateway (fast2sms.com). Used for the text-message alert when ApiKey is set.</summary>
    public class Fast2SmsOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>"q" = Quick SMS (no DLT template), "dlt" = approved DLT template route.</summary>
        public string Route { get; set; } = "q";
    }

    public class TwilioOptions
    {
        public string AccountSid { get; set; } = string.Empty;
        public string AuthToken { get; set; } = string.Empty;
        /// <summary>Twilio WhatsApp sender, e.g. +14155238886 (sandbox) or your approved number.</summary>
        public string WhatsAppFrom { get; set; } = string.Empty;
        /// <summary>
        /// Approved WhatsApp message template (Content SID, starts with "HX"). Required for live WhatsApp senders
        /// to message someone who has not written to you in the last 24 hours. Leave empty when using the sandbox.
        /// </summary>
        public string WhatsAppContentSid { get; set; } = string.Empty;
        /// <summary>Twilio SMS sender number or alphanumeric sender id.</summary>
        public string SmsFrom { get; set; } = string.Empty;
        /// <summary>SMS alerts are off by default (trial accounts cannot send them). Set true after upgrading.</summary>
        public bool EnableSms { get; set; }
    }
}
