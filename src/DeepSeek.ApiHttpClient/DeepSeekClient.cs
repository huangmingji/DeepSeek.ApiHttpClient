using System.Runtime.CompilerServices;
using DeepSeek.ApiHttpClient.Models;
using Newtonsoft.Json;
using Stargazer.Common.Extend;

namespace DeepSeek.ApiHttpClient;

public class DeepSeekClient()
{
    private string _apiKey = "sk-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
    private string _endpoint = "https://api.deepseek.com/chat/completions";
    private string _modelId = "deepseek-chat";
    private RequestBody _body = new();

    public static DeepSeekClient Create()
    {
        return new DeepSeekClient();
    }
    
    public DeepSeekClient SetApiKey(string apiKey)
    {
        _apiKey = apiKey;
        return this;
    }
    
    public DeepSeekClient SetModelId(string modelId)
    {
        _modelId = modelId;
        _body.Model = _modelId;
        return this;
    }
    public DeepSeekClient SetEndpoint(string endpoint)
    {
        _endpoint = endpoint;
        return this;
    }
    
    public DeepSeekClient SetTemperature(double temperature)
    {
        _body.Temperature = temperature;
        return this;
    }
    
    public DeepSeekClient SetMaxTokens(int maxTokens)
    {
        _body.MaxTokens = maxTokens;
        return this;
    }
    public DeepSeekClient SetTopP(double topP)
    {
        _body.TopP = topP;
        return this;
    }
    
    public DeepSeekClient SetChatMessageHistory(List<ChatMessageHistory> histories)
    {
        _body.Messages = histories;
        return this;
    }

    public async Task<ResponseBody> GetChatMessageContentsAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, _endpoint);
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", "Bearer " + _apiKey);

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
        var request = new HttpRequestMessage(HttpMethod.Post, _endpoint);
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", "Bearer " + _apiKey);
        
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

