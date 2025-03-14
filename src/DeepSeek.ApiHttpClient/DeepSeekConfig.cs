namespace DeepSeek.ApiHttpClient;

public class DeepSeekConfig
{
    public string ApiKey { get; set; } = "";
    public string Endpoint { get; set; } = "https://api.deepseek.com/chat/completions";
    public string Model { get; set; } = "deepseek-chat";
}