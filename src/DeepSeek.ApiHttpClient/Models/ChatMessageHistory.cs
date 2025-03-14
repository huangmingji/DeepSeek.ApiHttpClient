using Microsoft.SemanticKernel.ChatCompletion;
using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class ChatMessageHistory
{
    [JsonProperty("content")] public string Content { get; set; } = "";

    [JsonProperty("role")] public string Role { get; set; } = AuthorRole.Assistant.ToString();

    [JsonProperty("name")] public string? Name { get; set; }
    
    /// <summary>
    /// 此消息所响应的 tool call 的 ID。
    /// </summary>
    [JsonProperty("tool_call_id")] public string? ToolCallId { get; set; }
}