namespace PawfectMatch.Components.Handlers
{
    public class ValidateHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Example: Validate headers or add custom headers
            if (!request.Headers.Contains("Custom-Header"))
            {
                request.Headers.Add("Custom-Header", "HeaderValue");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
