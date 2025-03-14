using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class ResponseFormat
{
    [JsonProperty("type")] public string Type { get; set; } = "text";
}