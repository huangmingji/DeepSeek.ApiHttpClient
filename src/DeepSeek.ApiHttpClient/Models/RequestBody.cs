using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

/// <summary>
/// 请求体
/// </summary>
/// <see href="https://api-docs.deepseek.com/zh-cn/api/create-chat-completion"/>
public class RequestBody
{
    [JsonProperty("messages")] public List<ChatMessageHistory> Messages { get; set; } = new();

    [JsonProperty("model")] public string Model { get; set; } = "deepseek-chat";

    [JsonProperty("frequency_penalty")] public int FrequencyPenalty { get; set; }
    
    [JsonProperty("max_tokens")] public int MaxTokens { get; set; } = 4096;
    
    [JsonProperty("presence_penalty")] public int PresencePenalty { get; set; }

    [JsonProperty("response_format")] public ResponseFormat? ResponseFormat { get; set; }
    
    [JsonProperty("stop")] public Object? Stop { get; set; }
    
    [JsonProperty("stream")] public bool Stream { get; set; } = false;

    [JsonProperty("stream_options")] public StreamOptions? StreamOptions { get; set; }
    
    [JsonProperty("temperature")] public double? Temperature { get; set; }
    
    [JsonProperty("top_p")] public double? TopP { get; set; }
    
    [JsonProperty("tools")] public List<Tool>? Tools { get; set; }

    /// <summary>
    /// 控制模型调用 tool 的行为。
    /// none 意味着模型不会调用任何 tool，而是生成一条消息。
    /// auto 意味着模型可以选择生成一条消息或调用一个或多个 tool。
    /// required 意味着模型必须调用一个或多个 tool。
    /// 通过 {"type": "function", "function": {"name": "my_function"}} 指定特定 tool，会强制模型调用该 tool。
    /// 当没有 tool 时，默认值为 none。如果有 tool 存在，默认值为 auto。
    /// </summary>
    [JsonProperty("tool_choice")] public Object ToolChoice { get; set; } = "none";
    
    /// <summary>
    /// 是否返回所输出 token 的对数概率。如果为 true，则在 message 的 content 中返回每个输出 token 的对数概率。
    /// </summary>
    [JsonProperty("logprobs")] public bool? Logprobs { get; set; }
    
    /// <summary>
    /// 一个介于 0 到 20 之间的整数 N，指定每个输出位置返回输出概率 top N 的 token，且返回这些 token 的对数概率。指定此参数时，logprobs 必须为 true。
    /// </summary>
    [JsonProperty("top_logprobs")] public int? TopLogprobs { get; set; }
}