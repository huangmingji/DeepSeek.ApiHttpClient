using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class Tool
{
    [JsonProperty("type")] public string Type { get; set; } = "function";
    
    [JsonProperty("function")] public ToolFunction Function { get; set; } = new ToolFunction();
}