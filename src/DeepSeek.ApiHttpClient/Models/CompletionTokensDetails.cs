using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class CompletionTokensDetails
{
    /// <summary>
    /// 推理模型所产生的思维链 token 数量
    /// </summary>
    [JsonProperty("reasoning_tokens")] public int ReasoningTokens { get; set; }
}