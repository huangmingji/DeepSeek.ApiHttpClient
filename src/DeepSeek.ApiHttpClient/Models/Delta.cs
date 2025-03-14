using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class Delta
{
    [JsonProperty("content")]
    public string Content { get; set; } = "";

    [JsonProperty("role")]
    public string Role { get; set; } = "";

}