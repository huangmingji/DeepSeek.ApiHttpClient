using System.Runtime.CompilerServices;
using DeepSeek.ApiHttpClient.Models;
using Newtonsoft.Json;
using Stargazer.Common.Extend;

namespace DeepSeek.ApiHttpClient;

public class DeepSeekClient
{
    private readonly DeepSeekClientBuilder _builder;
    private readonly RequestBody _body = new();

    public DeepSeekClient(DeepSeekClientBuilder builder)
    {
        _builder = builder;
        _body.Model = builder.Model;
        _body.MaxTokens = builder.MaxTokens;
        _body.Temperature = builder.Temperature;
        _body.TopP = builder.TopP;
        _body.FrequencyPenalty = builder.FrequencyPenalty;
        _body.PresencePenalty = builder.PresencePenalty;
        _body.Logprobs = builder.Logprobs;
        _body.TopLogprobs = builder.TopLogprobs;
    }

    public DeepSeekClient SetChatMessageHistory(List<ChatMessageHistory> histories)
    {
        _body.Messages = histories;
        return this;
    }

    public async Task<ResponseBody> GetChatMessageContentsAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, _builder.Endpoint);
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", $"Bearer {_builder.ApiKey}");

        _body.Stream = false;
        var content = new StringContent(_body.SerializeObject(), null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<ResponseBody>(responseBody) ?? new ResponseBody();
    }

    public async IAsyncEnumerable<ResponseBody> GetStreamingChatMessageContentsAsync([EnumeratorCancellation] CancellationToken cancellationToken = new CancellationToken())
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, _builder.Endpoint);
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", $"Bearer {_builder.ApiKey}");
        
        _body.Stream = true;
        var content = new StringContent(_body.SerializeObject(), null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request, cancellationToken);
        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var reader = new StreamReader(stream);
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync(cancellationToken);
            if (string.IsNullOrEmpty(line) || line.StartsWith(":")) continue;
            if (line.StartsWith("data: "))
            {
                var jsonData = line["data: ".Length ..];
                if (jsonData == "[DONE]") break;
                yield return JsonConvert.DeserializeObject<ResponseBody>(jsonData) ?? new ResponseBody();
            }
        }
    }
}

