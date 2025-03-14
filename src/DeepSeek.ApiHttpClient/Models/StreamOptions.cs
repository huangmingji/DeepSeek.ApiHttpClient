using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class StreamOptions
{
    [JsonProperty("include_usage")] public bool IncludeUsage { get; set; } = true;
}