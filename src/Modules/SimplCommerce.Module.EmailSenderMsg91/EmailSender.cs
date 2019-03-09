using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SimplCommerce.Module.Core.Services;

namespace SimplCommerce.Module.EmailSenderMsg91
{
    public class EmailSender : IEmailSender
    {
        private readonly string _apiKey;
        private readonly string _from;
        private readonly string _apiURL;

        public EmailSender(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("Msg91:Email:ApiKey");
            _from = configuration.GetValue<string>("Msg91:Email:From");
            _apiURL = configuration.GetValue<string>("Msg91:Email:ApiURL");

            Contract.Requires(string.IsNullOrWhiteSpace(_apiKey));
            Contract.Requires(string.IsNullOrWhiteSpace(_from));
            Contract.Requires(string.IsNullOrWhiteSpace(_apiURL));
        }
        public async Task SendEmailAsync(string email, string subject, string message, bool isHtml = false)
        {
            Contract.Requires(string.IsNullOrWhiteSpace(email));
            Contract.Requires(string.IsNullOrWhiteSpace(subject));
            Contract.Requires(string.IsNullOrWhiteSpace(message));

            var apiPost = new StringBuilder(_apiURL);
            apiPost.Append("?body=");
            apiPost.Append(message);
            apiPost.Append("&subject=");
            apiPost.Append(subject);
            apiPost.Append("&to=");
            apiPost.Append(email);
            apiPost.Append("&from=");
            apiPost.Append(_from);
            apiPost.Append("&authkey=");
            apiPost.Append(_apiKey);

            var client = new HttpClient();
            await client.PostAsync(apiPost.ToString(), null);
        }
    }
}
