using Newtonsoft.Json;
using SSPRExternalApiExample.Api.Constants;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace SSPRExternalApiExample.Api.Helpers
{
    public class RequestHelper<T, T1> : Singleton<RequestHelper<T, T1>>
    {
        public async Task<T> PostAsync(T1 requestItem, string url, CancellationTokenSource continuationToken)
        {
            var resultItem = Activator.CreateInstance<T>();

            if (requestItem == null) return resultItem;

            var body = JsonConvert.SerializeObject(requestItem);

            if (string.IsNullOrEmpty(body)) return resultItem;

            try
            {
                var buffer = Encoding.UTF8.GetBytes(body);

                using (var byteContent = new ByteArrayContent(buffer))
                {
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (var clientHandler = new HttpClientHandler())
                    {
                        clientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, error) =>
                        {
                            return true;
                        };

                        using (var client = new HttpClient(clientHandler)
                        {
                            Timeout = TimeSpan.FromMilliseconds(60000)
                        })
                        {
                            client.DefaultRequestHeaders.Add(ExternalApiConstants.AuthorizationHeaderKey, ExternalApiConstants.ExternalApiKey);
                            using (var result = await client.PostAsync(url, byteContent, continuationToken.Token))
                            {
                                var postResult = await result.Content.ReadAsStringAsync();

                                if (string.IsNullOrEmpty(postResult)) return default;

                                resultItem = JsonHelper.Instance.DeserizalizeObject<T>(postResult);

                                return resultItem;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Write(log: $"Error URL: {url}\n\nRequestBody: {body}\n\nException: {ex}", methodName: System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, exceptionId: 7078);
            }

            return default;
        }

        public async Task<T> GetAsync(string url, CancellationTokenSource continuationToken)
        {
            var resultItem = Activator.CreateInstance<T>();

            try
            {
                using (var clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, error) =>
                    {
                        return true;
                    };

                    using (var client = new HttpClient(clientHandler)
                    {
                        Timeout = TimeSpan.FromMilliseconds(60000)
                    })
                    {
                        client.DefaultRequestHeaders.Add(ExternalApiConstants.AuthorizationHeaderKey, ExternalApiConstants.ExternalApiKey);

                        using (var result = await client.GetAsync(url, continuationToken.Token))
                        {
                            if (result.StatusCode != HttpStatusCode.OK) return default;

                            var postResult = await result.Content.ReadAsStringAsync();

                            if (string.IsNullOrEmpty(postResult)) return default;

                            resultItem = JsonHelper.Instance.DeserizalizeObject<T>(postResult);

                            return resultItem;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Write(log: $"Error URL: {url}\n\nException: {ex}", methodName: System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, exceptionId: 7081);
            }

            return default;
        }
    }
}
