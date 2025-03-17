using Stargazer.Common.Extend;

namespace DeepSeek.ApiHttpClient.Models;

public class DeepSeekClientBuilder
{
    public string ApiKey { get; set; } = "sk-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
    public string Endpoint { get; set; } = "https://api.deepseek.com/chat/completions";
    public string Model { get; set; } = "deepseek-chat";
    
    public int MaxTokens { get; set; } = 4096;

    public double? Temperature { get; set; } = 0.7;
    
    public double? TopP { get; set; }
    
    public int FrequencyPenalty { get; set; }
    
    public int PresencePenalty { get; set; }
    
    public bool? Logprobs { get; set; }
    
    public int? TopLogprobs { get; set; }
    
    public bool Stream { get; set; } = false;
    
    public static DeepSeekClientBuilder Build()
    {
        return new DeepSeekClientBuilder();
    }
    
    public DeepSeekClient CreateDeepSeekClient()
    {
        return new DeepSeekClient(this);
    }
    
    public DeepSeekClientBuilder SetApiKey(string apiKey)
    {
        if (apiKey.IsNullOrWhiteSpace())
        {
            throw new ArgumentException("ApiKey cannot be null or empty.");
        }
        ApiKey = apiKey;
        return this;
    }
    
    public DeepSeekClientBuilder SetEndpoint(string endpoint)
    {
        if (endpoint.IsNullOrWhiteSpace())
        {
            throw new ArgumentException("Endpoint cannot be null or empty.");
        }
        Endpoint = endpoint;
        return this;
    }
    
    public DeepSeekClientBuilder SetModel(string model)
    {
        if (model.IsNullOrWhiteSpace())
        {
            throw new ArgumentException("Model cannot be null or empty.");
        }
        Model = model;
        return this;
    }
    
    public DeepSeekClientBuilder SetMaxTokens(int maxTokens)
    {
        MaxTokens = maxTokens;
        return this;
    }
    
    public DeepSeekClientBuilder SetTemperature(double temperature)
    {
        Temperature = temperature;
        return this;
    }
    
    public DeepSeekClientBuilder SetTopP(double topP)
    {
        TopP = topP;
        return this;
    }
    
    public DeepSeekClientBuilder SetFrequencyPenalty(int frequencyPenalty)
    {
        FrequencyPenalty = frequencyPenalty;
        return this;
    }
    
    public DeepSeekClientBuilder SetPresencePenalty(int presencePenalty)
    {
        PresencePenalty = presencePenalty;
        return this;
    }
    
    public DeepSeekClientBuilder SetLogprobs(bool logprobs)
    {
        Logprobs = logprobs;
        return this;
    }
    
    public DeepSeekClientBuilder SetTopLogprobs(int topLogprobs)
    {
        TopLogprobs = topLogprobs;
        return this;
    }
    
    public DeepSeekClientBuilder SetSteam(bool stream)
    {
        Stream = stream;
        return this;
    }
}