using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class Choice
{
    /// <summary>
    /// 模型停止生成 token 的原因。
    /// stop：模型自然停止生成，或遇到 stop 序列中列出的字符串。
    /// length ：输出长度达到了模型上下文长度限制，或达到了 max_tokens 的限制。
    /// content_filter：输出内容因触发过滤策略而被过滤。
    /// insufficient_system_resource：系统推理资源不足，生成被打断。
    /// </summary>
    [JsonProperty("finish_reason")] public string FinishReason { get; set; } = "";
    
    /// <summary>
    /// 该 completion 在模型生成的 completion 的选择列表中的索引。
    /// </summary>
    [JsonProperty("index")] public int Index { get; set; }
    
    /// <summary>
    /// 该 choice 的对数概率信息。
    /// </summary>
    [JsonProperty("logprobs")] public object? Logprobs { get; set; }
    
    /// <summary>
    /// 模型生成的 completion 消息。
    /// </summary>
    [JsonProperty("message")] public Message Message { get; set; } = new Message();
    
    /// <summary>
    /// 模型生成的 delta 消息。
    /// </summary>
    [JsonProperty("delta")] public Delta? Delta { get; set; }
}