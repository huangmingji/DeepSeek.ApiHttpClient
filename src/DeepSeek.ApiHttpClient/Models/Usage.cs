using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class Usage
{
    /// <summary>
    /// 模型 completion 产生的 token 数
    /// </summary>
    [JsonProperty("completion_tenks")] public int CompletionTokens { get; set; }
    
    /// <summary>
    /// 用户 prompt 所包含的 token 数。该值等于 prompt_cache_hit_tokens + prompt_cache_miss_tokens
    /// </summary>
    [JsonProperty("prompt_tokens")] public int PromptTokens { get; set; }
    
    /// <summary>
    /// 用户 prompt 中，命中上下文缓存的 token 数。
    /// </summary>
    [JsonProperty("prompt_cache_hit_tokens")] public int PromptCacheHitTokens { get; set; }
    
    /// <summary>
    /// 用户 prompt 中，未命中上下文缓存的 token 数。
    /// </summary>
    [JsonProperty("prompt_cache_miss_tokens")] public int PromptCacheMissTokens { get; set; }
    
    /// <summary>
    /// 该请求中，所有 token 的数量（prompt + completion）。
    /// </summary>
    [JsonProperty("total_tokens")] public int TotalTokens { get; set; }
    
    /// <summary>
    /// completion tokens 的详细信息。
    /// </summary>
    [JsonProperty("completion_tokens_details")] public CompletionTokensDetails CompletionTokensDetails { get; set; }
}