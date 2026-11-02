using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ADomicilioWebApi.MessageHandlers
{
    public class APIKeyMessageHandler : DelegatingHandler
    {
        private string ApiKey = "";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            var produccion = bool.Parse(ConfigurationManager.AppSettings["produccion"] as string);
            if (!produccion)
            {
                ApiKey = ConfigurationManager.AppSettings["testingApiKey"] as string;
            }
            else
            {
                ApiKey = ConfigurationManager.AppSettings["prodApiKey"] as string;
            }

            bool validKey = false;
            IEnumerable<string> requestHeaders;
            var checkApiKeyExists = httpRequestMessage.Headers.TryGetValues("APIKey", out requestHeaders);
            if (checkApiKeyExists)
            {
                if (requestHeaders.FirstOrDefault().Equals(ApiKey))
                {
                    validKey = true;
                }
            }

            if (!validKey)
            {
                return httpRequestMessage.CreateResponse(HttpStatusCode.Forbidden, "Api Key inválido.");
            }

            var response = await base.SendAsync(httpRequestMessage, cancellationToken);
            return response;
        }
    }
}