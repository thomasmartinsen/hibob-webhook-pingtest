using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

public class Pingtest
{
    private readonly ILogger _logger;

    public Pingtest(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Pingtest>();
    }

    [Function("bob-webhook-pingtest")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        _logger.LogInformation("bob-webhook-pingtest");

        var content = await req.ReadAsStringAsync();
        _ = content ?? throw new ArgumentNullException(nameof(content));

        var result = content == "Ping test" ? "verified" : "not verified";

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString($"ping test {result}!");

        return response;
    }
}
