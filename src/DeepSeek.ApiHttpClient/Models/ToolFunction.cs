using Newtonsoft.Json;

namespace DeepSeek.ApiHttpClient.Models;

public class ToolFunction
{
    /// <summary>
    /// 要调用的 function 名称。必须由 a-z、A-Z、0-9 字符组成，或包含下划线和连字符，最大长度为 64 个字符。
    /// </summary>
    [JsonProperty("name")] public string Name { get; set; } = "";
    
    /// <summary>
    /// function 的功能描述，供模型理解何时以及如何调用该 function。
    /// </summary>
    [JsonProperty("description")] public string Description { get; set; } = "";

    /// <summary>
    /// function 的输入参数，以 JSON Schema 对象描述。请参阅 Function Calling 指南获取示例，并参阅JSON Schema 参考了解有关格式的文档。省略 parameters 会定义一个参数列表为空的
    /// </summary>
    [JsonProperty("parameters")] public Object Parameters { get; set; }
}