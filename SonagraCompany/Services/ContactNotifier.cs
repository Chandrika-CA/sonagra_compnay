using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using SonagraCompany.Models;

namespace SonagraCompany.Services
{
    public record NotificationResult(bool EmailSent, bool WhatsAppSent, bool SmsSent)
    {
        public bool AnySent => EmailSent || WhatsAppSent || SmsSent;
    }

    /// <summary>Delivers a website enquiry by e-mail, WhatsApp and SMS.</summary>
    public class ContactNotifier
    {
        private readonly ContactOptions _options;
        private readonly HttpClient _http;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ContactNotifier> _logger;

        public ContactNotifier(IOptions<ContactOptions> options, HttpClient http, IWebHostEnvironment env, ILogger<ContactNotifier> logger)
        {
            _options = options.Value;
            _http = http;
            _env = env;
            _logger = logger;
        }

        public async Task<NotificationResult> SendAsync(ContactEnquiry enquiry)
        {
            var email = await TrySendAsync("e-mail", () => SendEmailAsync(enquiry));
            var whatsApp = await TrySendAsync("WhatsApp", () => SendTwilioAsync(enquiry, whatsApp: true));
            var sms = await TrySendAsync("SMS", () => SendSmsAsync(enquiry));
            return new NotificationResult(email, whatsApp, sms);
        }

        private async Task<bool> TrySendAsync(string channel, Func<Task<bool>> send)
        {
            try
            {
                var sent = await send();
                if (!sent)
                {
                    _logger.LogWarning("Enquiry {Channel} not sent: channel is not configured.", channel);
                }

                return sent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Enquiry {Channel} failed.", channel);
                return false;
            }
        }

        // ---------- E-mail ----------

        private async Task<bool> SendEmailAsync(ContactEnquiry enquiry)
        {
            var smtp = _options.Smtp;
            if (string.IsNullOrWhiteSpace(smtp.Host) || string.IsNullOrWhiteSpace(smtp.User)
                || string.IsNullOrWhiteSpace(smtp.Password) || string.IsNullOrWhiteSpace(_options.NotifyEmail))
            {
                return false;
            }

            var from = string.IsNullOrWhiteSpace(smtp.From) ? smtp.User : smtp.From;
            using var message = new MailMessage
            {
                From = new MailAddress(from, smtp.FromName),
                Subject = $"New website enquiry from {SingleLine(enquiry.Name)}",
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8
            };
            message.To.Add(_options.NotifyEmail);
            message.ReplyToList.Add(new MailAddress(enquiry.Email, SingleLine(enquiry.Name)));

            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(BuildPlainText(enquiry), Encoding.UTF8, "text/plain"));

            var htmlView = AlternateView.CreateAlternateViewFromString(BuildEmailHtml(enquiry), Encoding.UTF8, "text/html");
            var logoPath = Path.Combine(_env.WebRootPath, "images", "sonagra-logo.png");
            if (File.Exists(logoPath))
            {
                htmlView.LinkedResources.Add(new LinkedResource(logoPath, "image/png") { ContentId = "sonagra-logo" });
            }

            message.AlternateViews.Add(htmlView);

            using var client = new SmtpClient(smtp.Host, smtp.Port)
            {
                EnableSsl = smtp.EnableSsl,
                Credentials = new NetworkCredential(smtp.User, smtp.Password)
            };
            await client.SendMailAsync(message);
            return true;
        }

        private static string BuildPlainText(ContactEnquiry e) =>
            $"New website enquiry - Sonagra & Company\n\nName: {SingleLine(e.Name)}\nEmail: {e.Email}\nPhone: {PhoneOrDash(e)}\nReceived: {Now()}\n\nMessage:\n{e.Message}\n";

        private static string BuildEmailHtml(ContactEnquiry e)
        {
            static string H(string? s) => WebUtility.HtmlEncode(s ?? string.Empty);
            var message = H(e.Message).Replace("\r\n", "\n").Replace("\n", "<br />");
            var phone = PhoneOrDash(e);
            var phoneHtml = e.Phone is { Length: > 0 } ? $"<a href=\"tel:+91{H(e.Phone)}\" style=\"color:#0b1e3b;text-decoration:none;\">+91 {H(e.Phone)}</a>" : "&mdash;";

            string Row(string label, string valueHtml) => $@"
              <tr>
                <td style=""padding:14px 0;border-bottom:1px solid #e6eaf0;width:130px;vertical-align:top;font-size:12px;font-weight:700;letter-spacing:.08em;text-transform:uppercase;color:#c99a42;"">{label}</td>
                <td style=""padding:14px 0;border-bottom:1px solid #e6eaf0;vertical-align:top;font-size:15px;line-height:1.6;color:#17243a;"">{valueHtml}</td>
              </tr>";

            return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
<meta charset=""utf-8"" />
<meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
<title>New website enquiry</title>
</head>
<body style=""margin:0;padding:0;background:#f4f6f9;font-family:'Segoe UI',Arial,Helvetica,sans-serif;"">
  <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""background:#f4f6f9;padding:28px 12px;"">
    <tr><td align=""center"">
      <table role=""presentation"" width=""600"" cellspacing=""0"" cellpadding=""0"" style=""width:100%;max-width:600px;background:#ffffff;border-top:4px solid #c99a42;box-shadow:0 6px 24px rgba(11,30,59,.08);"">
        <tr><td style=""padding:28px 32px 20px;"" align=""left"">
          <img src=""cid:sonagra-logo"" alt=""Sonagra &amp; Company"" width=""190"" style=""display:block;border:0;height:auto;max-width:190px;"" />
        </td></tr>
        <tr><td style=""background:#0b1e3b;padding:26px 32px;"">
          <div style=""font-size:12px;font-weight:700;letter-spacing:.14em;text-transform:uppercase;color:#e7c57d;"">Website enquiry</div>
          <div style=""margin-top:8px;font-family:Georgia,'Times New Roman',serif;font-size:26px;line-height:1.25;color:#ffffff;"">You have a new enquiry</div>
        </td></tr>
        <tr><td style=""padding:26px 32px 8px;"">
          <p style=""margin:0 0 6px;font-size:15px;line-height:1.6;color:#68758a;"">Someone has just submitted the &ldquo;How can we help?&rdquo; form on the Sonagra &amp; Company website.</p>
          <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""margin-top:10px;"">
            {Row("Name", H(SingleLine(e.Name)))}
            {Row("Email", $"<a href=\"mailto:{H(e.Email)}\" style=\"color:#0b1e3b;text-decoration:none;\">{H(e.Email)}</a>")}
            {Row("Phone", phoneHtml)}
            {Row("Received", H(Now()))}
          </table>
        </td></tr>
        <tr><td style=""padding:8px 32px 26px;"">
          <div style=""font-size:12px;font-weight:700;letter-spacing:.08em;text-transform:uppercase;color:#c99a42;margin:14px 0 10px;"">Message</div>
          <div style=""padding:18px 20px;background:#f7f8fa;border-left:3px solid #c99a42;font-size:15px;line-height:1.7;color:#17243a;"">{message}</div>
          <table role=""presentation"" cellspacing=""0"" cellpadding=""0"" style=""margin-top:24px;""><tr>
            <td style=""background:#e7c57d;"">
              <a href=""mailto:{H(e.Email)}?subject=Re:%20Your%20enquiry%20to%20Sonagra%20%26%20Company"" style=""display:inline-block;padding:13px 24px;font-size:13px;font-weight:700;color:#0b1e3b;text-decoration:none;"">Reply to {H(SingleLine(e.Name))} &rarr;</a>
            </td>
          </tr></table>
        </td></tr>
        <tr><td style=""background:#07152b;padding:22px 32px;font-size:12px;line-height:1.7;color:#b9c7da;"">
          <strong style=""color:#ffffff;"">Sonagra &amp; Company</strong><br />
          326, 3rd Floor, Star Arcade, Sanala Road, Morbi &ndash; 363641, Gujarat, India<br />
          +91 75670 96966 &nbsp;|&nbsp; cs@sonagra.com
        </td></tr>
      </table>
      <p style=""max-width:600px;margin:14px 0 0;font-size:11px;line-height:1.5;color:#8b97a8;"">This message was generated automatically from the website contact form. Reply directly to this e-mail to respond to the sender.</p>
    </td></tr>
  </table>
</body>
</html>";
        }

        // ---------- Job applications (Careers page) ----------

        /// <summary>Sends a job application: e-mail with the resume attached, plus WhatsApp / SMS alerts.</summary>
        public async Task<NotificationResult> SendCareerAsync(CareerApplicationRequest application, string? resumeName, byte[]? resumeBytes)
        {
            var note = SingleLine(application.Message ?? string.Empty);
            var alert = new ContactEnquiry
            {
                Name = application.Name,
                Email = application.Email,
                Phone = application.Phone,
                Message = ("Job application for " + SingleLine(application.Role) + (note.Length > 0 ? ". " + note : "") + (resumeName is null ? "" : " (resume attached to e-mail)"))
            };

            var email = await TrySendAsync("career e-mail", () => SendCareerEmailAsync(application, resumeName, resumeBytes));
            var whatsApp = await TrySendAsync("career WhatsApp", () => SendTwilioAsync(alert, whatsApp: true, title: "New job application"));
            var sms = await TrySendAsync("career SMS", () => SendSmsAsync(alert, "Sonagra job application"));
            return new NotificationResult(email, whatsApp, sms);
        }

        private async Task<bool> SendCareerEmailAsync(CareerApplicationRequest a, string? resumeName, byte[]? resumeBytes)
        {
            var smtp = _options.Smtp;
            if (string.IsNullOrWhiteSpace(smtp.Host) || string.IsNullOrWhiteSpace(smtp.User)
                || string.IsNullOrWhiteSpace(smtp.Password) || string.IsNullOrWhiteSpace(_options.NotifyEmail))
            {
                return false;
            }

            var from = string.IsNullOrWhiteSpace(smtp.From) ? smtp.User : smtp.From;
            using var message = new MailMessage
            {
                From = new MailAddress(from, smtp.FromName),
                Subject = $"New job application: {SingleLine(a.Role)} - {SingleLine(a.Name)}",
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8
            };
            message.To.Add(_options.NotifyEmail);
            message.ReplyToList.Add(new MailAddress(a.Email, SingleLine(a.Name)));

            var plain = $"New job application - Sonagra & Company\n\nRole: {SingleLine(a.Role)}\nName: {SingleLine(a.Name)}\nEmail: {a.Email}\nPhone: {a.Phone}\nResume: {(resumeName ?? "not attached")}\nReceived: {Now()}\n\nMessage:\n{a.Message}\n";
            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plain, Encoding.UTF8, "text/plain"));

            var htmlView = AlternateView.CreateAlternateViewFromString(BuildCareerEmailHtml(a, resumeName), Encoding.UTF8, "text/html");
            var logoPath = Path.Combine(_env.WebRootPath, "images", "sonagra-logo.png");
            if (File.Exists(logoPath))
            {
                htmlView.LinkedResources.Add(new LinkedResource(logoPath, "image/png") { ContentId = "sonagra-logo" });
            }

            message.AlternateViews.Add(htmlView);

            if (resumeName is not null && resumeBytes is { Length: > 0 })
            {
                message.Attachments.Add(new Attachment(new MemoryStream(resumeBytes), resumeName, ResumeMimeType(resumeName)));
            }

            using var client = new SmtpClient(smtp.Host, smtp.Port)
            {
                EnableSsl = smtp.EnableSsl,
                Credentials = new NetworkCredential(smtp.User, smtp.Password)
            };
            await client.SendMailAsync(message);
            return true;
        }

        private static string ResumeMimeType(string fileName) => Path.GetExtension(fileName).ToLowerInvariant() switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            _ => "application/octet-stream"
        };

        private static string BuildCareerEmailHtml(CareerApplicationRequest a, string? resumeName)
        {
            static string H(string? s) => WebUtility.HtmlEncode(s ?? string.Empty);
            var message = string.IsNullOrWhiteSpace(a.Message) ? "&mdash;" : H(a.Message).Replace("\r\n", "\n").Replace("\n", "<br />");

            string Row(string label, string valueHtml) => $@"
              <tr>
                <td style=""padding:14px 0;border-bottom:1px solid #e6eaf0;width:130px;vertical-align:top;font-size:12px;font-weight:700;letter-spacing:.08em;text-transform:uppercase;color:#c99a42;"">{label}</td>
                <td style=""padding:14px 0;border-bottom:1px solid #e6eaf0;vertical-align:top;font-size:15px;line-height:1.6;color:#17243a;"">{valueHtml}</td>
              </tr>";

            return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
<meta charset=""utf-8"" />
<meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
<title>New job application</title>
</head>
<body style=""margin:0;padding:0;background:#f4f6f9;font-family:'Segoe UI',Arial,Helvetica,sans-serif;"">
  <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""background:#f4f6f9;padding:28px 12px;"">
    <tr><td align=""center"">
      <table role=""presentation"" width=""600"" cellspacing=""0"" cellpadding=""0"" style=""width:100%;max-width:600px;background:#ffffff;border-top:4px solid #c99a42;box-shadow:0 6px 24px rgba(11,30,59,.08);"">
        <tr><td style=""padding:28px 32px 20px;"" align=""left"">
          <img src=""cid:sonagra-logo"" alt=""Sonagra &amp; Company"" width=""190"" style=""display:block;border:0;height:auto;max-width:190px;"" />
        </td></tr>
        <tr><td style=""background:#0b1e3b;padding:26px 32px;"">
          <div style=""font-size:12px;font-weight:700;letter-spacing:.14em;text-transform:uppercase;color:#e7c57d;"">Careers</div>
          <div style=""margin-top:8px;font-family:Georgia,'Times New Roman',serif;font-size:26px;line-height:1.25;color:#ffffff;"">New job application</div>
        </td></tr>
        <tr><td style=""padding:26px 32px 8px;"">
          <p style=""margin:0 0 6px;font-size:15px;line-height:1.6;color:#68758a;"">A candidate has applied through the Careers page of the Sonagra &amp; Company website.</p>
          <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""margin-top:10px;"">
            {Row("Role", H(SingleLine(a.Role)))}
            {Row("Name", H(SingleLine(a.Name)))}
            {Row("Email", $"<a href=\"mailto:{H(a.Email)}\" style=\"color:#0b1e3b;text-decoration:none;\">{H(a.Email)}</a>")}
            {Row("Phone", $"<a href=\"tel:+91{H(a.Phone)}\" style=\"color:#0b1e3b;text-decoration:none;\">+91 {H(a.Phone)}</a>")}
            {Row("Resume", resumeName is null ? "Not attached" : "Attached: " + H(resumeName))}
            {Row("Received", H(Now()))}
          </table>
        </td></tr>
        <tr><td style=""padding:8px 32px 26px;"">
          <div style=""font-size:12px;font-weight:700;letter-spacing:.08em;text-transform:uppercase;color:#c99a42;margin:14px 0 10px;"">Message</div>
          <div style=""padding:18px 20px;background:#f7f8fa;border-left:3px solid #c99a42;font-size:15px;line-height:1.7;color:#17243a;"">{message}</div>
          <table role=""presentation"" cellspacing=""0"" cellpadding=""0"" style=""margin-top:24px;""><tr>
            <td style=""background:#e7c57d;"">
              <a href=""mailto:{H(a.Email)}?subject=Re:%20Your%20application%20to%20Sonagra%20%26%20Company"" style=""display:inline-block;padding:13px 24px;font-size:13px;font-weight:700;color:#0b1e3b;text-decoration:none;"">Reply to {H(SingleLine(a.Name))} &rarr;</a>
            </td>
          </tr></table>
        </td></tr>
        <tr><td style=""background:#07152b;padding:22px 32px;font-size:12px;line-height:1.7;color:#b9c7da;"">
          <strong style=""color:#ffffff;"">Sonagra &amp; Company</strong><br />
          326, 3rd Floor, Star Arcade, Sanala Road, Morbi &ndash; 363641, Gujarat, India<br />
          +91 75670 96966 &nbsp;|&nbsp; cs@sonagra.com
        </td></tr>
      </table>
      <p style=""max-width:600px;margin:14px 0 0;font-size:11px;line-height:1.5;color:#8b97a8;"">This message was generated automatically from the Careers page. The candidate's resume is attached to this e-mail when one was uploaded.</p>
    </td></tr>
  </table>
</body>
</html>";
        }

        // ---------- SMS (Fast2SMS, India) ----------

        private async Task<bool> SendSmsAsync(ContactEnquiry enquiry, string label = "Sonagra enquiry")
        {
            if (!string.IsNullOrWhiteSpace(_options.Fast2Sms.ApiKey))
            {
                return await SendFast2SmsAsync(enquiry, label);
            }

            // No Indian SMS gateway configured: fall back to Twilio SMS (only when explicitly enabled).
            return await SendTwilioAsync(enquiry, whatsApp: false);
        }

        private async Task<bool> SendFast2SmsAsync(ContactEnquiry enquiry, string label)
        {
            var number = string.Concat(_options.NotifyPhone.Where(char.IsDigit).TakeLast(10));
            if (number.Length != 10)
            {
                return false;
            }

            var text = $"{label}: {SingleLine(enquiry.Name)}, {PhoneOrDash(enquiry)}. {SingleLine(enquiry.Message)}";
            if (text.Length > 300)
            {
                text = text[..297] + "...";
            }

            var url = "https://www.fast2sms.com/dev/bulkV2"
                + $"?route={Uri.EscapeDataString(_options.Fast2Sms.Route)}"
                + $"&message={Uri.EscapeDataString(text)}"
                + $"&numbers={number}";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.TryAddWithoutValidation("authorization", _options.Fast2Sms.ApiKey.Trim());

            using var response = await _http.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            var ok = false;
            try
            {
                using var json = System.Text.Json.JsonDocument.Parse(body);
                ok = response.IsSuccessStatusCode
                    && json.RootElement.TryGetProperty("return", out var ret)
                    && ret.ValueKind == System.Text.Json.JsonValueKind.True;
            }
            catch (System.Text.Json.JsonException)
            {
            }

            if (!ok)
            {
                throw new InvalidOperationException($"Fast2SMS returned {(int)response.StatusCode}: {body}");
            }

            _logger.LogInformation("Fast2SMS accepted the text message: {Body}", body);
            return true;
        }

        // ---------- WhatsApp & SMS (Twilio) ----------

        //private async Task<bool> SendTwilioAsync(ContactEnquiry enquiry, bool whatsApp)
        //{
        //    var twilio = _options.Twilio;
        //    var from = (whatsApp ? twilio.WhatsAppFrom : twilio.SmsFrom).Trim();
        //    if (from.StartsWith("whatsapp:", StringComparison.OrdinalIgnoreCase))
        //    {
        //        from = from["whatsapp:".Length..].Trim();
        //    }

        //    if (string.IsNullOrWhiteSpace(twilio.AccountSid) || string.IsNullOrWhiteSpace(twilio.AuthToken)
        //        || string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(_options.NotifyPhone))
        //    {
        //        return false;
        //    }

        //    var to = "+91" + string.Concat(_options.NotifyPhone.Where(char.IsDigit).TakeLast(10));
        //    var fields = new Dictionary<string, string>
        //    {
        //        ["From"] = whatsApp ? $"whatsapp:{from}" : from,
        //        ["To"] = whatsApp ? $"whatsapp:{to}" : to
        //    };

        //    if (whatsApp && !string.IsNullOrWhiteSpace(twilio.WhatsAppContentSid))
        //    {
        //        // Business-initiated WhatsApp messages must use an approved template.
        //        // Template variables: {{1}} name, {{2}} phone, {{3}} email, {{4}} message.
        //        fields["ContentSid"] = twilio.WhatsAppContentSid.Trim();
        //        fields["ContentVariables"] = System.Text.Json.JsonSerializer.Serialize(new Dictionary<string, string>
        //        {
        //            ["1"] = TemplateValue(enquiry.Name, 60),
        //            ["2"] = TemplateValue(PhoneOrDash(enquiry), 20),
        //            ["3"] = TemplateValue(enquiry.Email, 80),
        //            ["4"] = TemplateValue(enquiry.Message, 300)
        //        });
        //    }
        //    else
        //    {
        //        fields["Body"] = BuildShortMessage(enquiry);
        //    }

        //    _logger.LogInformation("Twilio {Channel} request from {From} using {Mode}.",
        //        whatsApp ? "WhatsApp" : "SMS", fields["From"], fields.ContainsKey("ContentSid") ? "template (ContentSid)" : "free-form Body");

        //    using var request = new HttpRequestMessage(HttpMethod.Post,
        //        $"https://api.twilio.com/2010-04-01/Accounts/{Uri.EscapeDataString(twilio.AccountSid)}/Messages.json")
        //    {
        //        Content = new FormUrlEncodedContent(fields)
        //    };
        //    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{twilio.AccountSid}:{twilio.AuthToken}"));
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

        //    using var response = await _http.SendAsync(request);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var detail = await response.Content.ReadAsStringAsync();
        //        throw new InvalidOperationException($"Twilio returned {(int)response.StatusCode}: {detail}");
        //    }

        //    // Twilio accepting the request does not mean WhatsApp delivered it, so log the real status a moment later.
        //    var sid = System.Text.Json.JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement
        //        .GetProperty("sid").GetString();
        //    _logger.LogInformation("Twilio {Channel} accepted the message. SID {Sid}.", whatsApp ? "WhatsApp" : "SMS", sid);
        //    _ = LogDeliveryStatusAsync(twilio, sid, whatsApp ? "WhatsApp" : "SMS");

        //    return true;
        //}


        private async Task<bool> SendTwilioAsync(ContactEnquiry enquiry, bool whatsApp, string title = "New website enquiry")
        {
            var twilio = _options.Twilio;
            if (!whatsApp && !twilio.EnableSms)
            {
                // Trial accounts only allow predefined SMS templates (error 572006), so SMS stays off until enabled.
                _logger.LogInformation("SMS skipped: Contact:Twilio:EnableSms is false.");
                return false;
            }

            var from = (whatsApp ? twilio.WhatsAppFrom : twilio.SmsFrom).Trim();
            if (from.StartsWith("whatsapp:", StringComparison.OrdinalIgnoreCase))
            {
                from = from["whatsapp:".Length..].Trim();
            }

            if (string.IsNullOrWhiteSpace(twilio.AccountSid) || string.IsNullOrWhiteSpace(twilio.AuthToken)
                || string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(_options.NotifyPhone))
            {
                return false;
            }

            var to = "+91" + string.Concat(_options.NotifyPhone.Where(char.IsDigit).TakeLast(10));
            var fields = new Dictionary<string, string>
            {
                ["From"] = whatsApp ? $"whatsapp:{from}" : from,
                ["To"] = whatsApp ? $"whatsapp:{to}" : to
            };

            if (whatsApp)
            {
                // WhatsApp needs an approved template (ContentSid). Trial accounts cannot create or use custom templates
                // (Twilio errors 21654 / 21655 / 572006), so until the account is upgraded this channel is skipped quietly.
                if (string.IsNullOrWhiteSpace(twilio.WhatsAppContentSid))
                {
                    _logger.LogInformation("WhatsApp skipped: no approved template (Contact:Twilio:WhatsAppContentSid) is configured.");
                    return false;
                }

                var contentSid = twilio.WhatsAppContentSid.Trim();
                fields["ContentSid"] = contentSid;

                var values = new Dictionary<string, string>
                {
                    ["1"] = TemplateValue(enquiry.Name, 60),
                    ["2"] = TemplateValue(PhoneOrDash(enquiry), 20),
                    ["3"] = TemplateValue(enquiry.Email, 80),
                    ["4"] = TemplateValue(enquiry.Message, 300)
                };
                var keys = await GetTemplateVariableKeysAsync(twilio, contentSid) ?? values.Keys.ToArray();
                if (keys.Count > 0)
                {
                    fields["ContentVariables"] = System.Text.Json.JsonSerializer.Serialize(
                        keys.ToDictionary(k => k, k => values.TryGetValue(k, out var v) ? v : "-"));
                }
            }
            else
            {
                fields["Body"] = BuildShortMessage(enquiry, title);
            }

            _logger.LogInformation("Twilio {Channel} request from {From}.", whatsApp ? "WhatsApp" : "SMS", fields["From"]);

            using var request = new HttpRequestMessage(HttpMethod.Post,
                $"https://api.twilio.com/2010-04-01/Accounts/{Uri.EscapeDataString(twilio.AccountSid)}/Messages.json")
            {
                Content = new FormUrlEncodedContent(fields)
            };

            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{twilio.AccountSid}:{twilio.AuthToken}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            using var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var detail = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Twilio returned {(int)response.StatusCode}: {detail}");
            }

            var sid = System.Text.Json.JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement
                .GetProperty("sid").GetString();
            _logger.LogInformation("Twilio {Channel} accepted the message. SID {Sid}.", whatsApp ? "WhatsApp" : "SMS", sid);
            _ = LogDeliveryStatusAsync(twilio, sid, whatsApp ? "WhatsApp" : "SMS");

            return true;
        }



        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, IReadOnlyList<string>> TemplateKeys = new();

        /// <summary>Reads which {{n}} variables the Twilio template uses, so we send exactly those.</summary>
        private async Task<IReadOnlyList<string>?> GetTemplateVariableKeysAsync(TwilioOptions twilio, string contentSid)
        {
            if (TemplateKeys.TryGetValue(contentSid, out var cached))
            {
                return cached;
            }

            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get,
                    $"https://content.twilio.com/v1/Content/{Uri.EscapeDataString(contentSid)}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{twilio.AccountSid}:{twilio.AuthToken}")));
                using var response = await _http.SendAsync(request);
                var text = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Could not read Twilio template {Sid}: {Status} {Body}", contentSid, (int)response.StatusCode, text);
                    return null;
                }

                using var json = System.Text.Json.JsonDocument.Parse(text);
                var keys = new List<string>();
                if (json.RootElement.TryGetProperty("variables", out var vars) && vars.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    keys.AddRange(vars.EnumerateObject().Select(p => p.Name));
                }

                _logger.LogInformation("Twilio template {Sid} uses variables: {Keys}", contentSid, keys.Count == 0 ? "(none)" : string.Join(", ", keys));
                TemplateKeys[contentSid] = keys;
                return keys;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not read Twilio template {Sid}.", contentSid);
                return null;
            }
        }

        private async Task LogDeliveryStatusAsync(TwilioOptions twilio, string? sid, string channel)
        {
            if (string.IsNullOrEmpty(sid))
            {
                return;
            }

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(4));
                using var request = new HttpRequestMessage(HttpMethod.Get,
                    $"https://api.twilio.com/2010-04-01/Accounts/{Uri.EscapeDataString(twilio.AccountSid)}/Messages/{Uri.EscapeDataString(sid)}.json");
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{twilio.AccountSid}:{twilio.AuthToken}")));
                using var response = await _http.SendAsync(request);
                using var json = System.Text.Json.JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                var root = json.RootElement;
                var status = root.TryGetProperty("status", out var s) ? s.GetString() : "?";
                var code = root.TryGetProperty("error_code", out var c) && c.ValueKind != System.Text.Json.JsonValueKind.Null ? c.ToString() : "none";
                var text = root.TryGetProperty("error_message", out var m) && m.ValueKind != System.Text.Json.JsonValueKind.Null ? m.GetString() : string.Empty;
                _logger.LogInformation("Twilio {Channel} status for {Sid}: {Status}, error code {Code} {Text}", channel, sid, status, code, text);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not read Twilio {Channel} delivery status for {Sid}.", channel, sid);
            }
        }

        private static string BuildShortMessage(ContactEnquiry e, string title = "New website enquiry")
        {
            var message = e.Message.Length > 400 ? e.Message[..400] + "..." : e.Message;
            return $"{title} - Sonagra & Company\nName: {SingleLine(e.Name)}\nPhone: {PhoneOrDash(e)}\nEmail: {e.Email}\nMessage: {message}";
        }

        // WhatsApp template variables cannot contain new lines, tabs or runs of spaces.
        private static string TemplateValue(string value, int maxLength)
        {
            var clean = System.Text.RegularExpressions.Regex.Replace(value, @"\s+", " ").Trim();
            if (clean.Length == 0)
            {
                return "-";
            }

            return clean.Length > maxLength ? clean[..maxLength] + "..." : clean;
        }

        private static string PhoneOrDash(ContactEnquiry e) => string.IsNullOrWhiteSpace(e.Phone) ? "-" : e.Phone;

        private static string SingleLine(string s) => s.Replace("\r", " ").Replace("\n", " ").Trim();

        private static string Now() => DateTime.Now.ToString("dd MMM yyyy, hh:mm tt");
    }
}
