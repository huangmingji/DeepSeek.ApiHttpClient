using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class ResponseBody
{
    [JsonProperty("id")] public string Id { get; set; } = "";

    [JsonProperty("choices")] public List<Choice> Choices { get; set; } = new();
    
    [JsonProperty("created")] public int Created { get; set; }
    
    [JsonProperty("model")] public string Model { get; set; } = "";

    [JsonProperty("system_fingerprint")] public string SystemFingerprint { get; set; } = "";
    
    [JsonProperty("usage")] public Usage Usage { get; set; } = new Usage();
}