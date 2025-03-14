using Microsoft.SemanticKernel.ChatCompletion;
using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class Message
{
    /// <summary>
    /// 该 completion 的内容
    /// </summary>
    [JsonProperty("content")] public string Content { get; set; } = "";
    
    /// <summary>
    /// 仅适用于 deepseek-reasoner 模型。内容为 assistant 消息中在最终答案之前的推理内容。
    /// </summary>
    [JsonProperty("reasoning_content")]  public string ReasoningContent { get; set; } = "";
    
    /// <summary>
    /// 该 completion 的角色
    /// </summary>
    [JsonProperty("role")] public string Role { get; set; } = AuthorRole.Assistant.ToString();
}