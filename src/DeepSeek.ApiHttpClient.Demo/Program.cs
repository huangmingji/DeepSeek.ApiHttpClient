// See https://aka.ms/new-console-template for more information

using System.ClientModel;
using DeepSeek.ApiHttpClient;
using DeepSeek.ApiHttpClient.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI;

string apiKey = "sk-xxxxxxxxxx";
string endpoint = "https://api.deepseek.com/chat/completions";
string modelId = "deepseek-chat";

List<ChatMessageHistory> histories = new List<ChatMessageHistory>()
{
    new ChatMessageHistory()
    {
        Role = "user",
        Content = "Hello, how are you?"
    },
    new ChatMessageHistory()
    {
        Role = "assistant",
        Content = "I am good, how are you?"
    },
    new ChatMessageHistory()
    {
        Role = "user",
        Content = "What is your name?"
    }
};

// Create a chat message
var chatMessage = await DeepSeekClientBuilder.Build()
    .SetApiKey(apiKey)
    .SetEndpoint(endpoint)
    .SetModel(modelId)
    .CreateDeepSeekClient()
    .SetChatMessageHistory(histories)
    .GetChatMessageContentsAsync();

Console.WriteLine("Create a chat message");
Console.WriteLine("😀User >> "+ histories.Last().Content);
Console.WriteLine("👨Assistant >> "+ chatMessage.Choices.Last().Message.Content);

// Create a streaming chat message
var streamingChatMessage = DeepSeekClientBuilder.Build()
    .SetApiKey(apiKey)
    .SetEndpoint(endpoint)
    .SetModel(modelId)
    .CreateDeepSeekClient()
    .SetChatMessageHistory(histories)
    .GetStreamingChatMessageContentsAsync();

Console.WriteLine("Create a streaming chat message");
Console.WriteLine("😀User >> "+ histories.Last().Content);
Console.Write("👨Assistant >> ");
await foreach (var item in streamingChatMessage)
{
    Thread.Sleep(200);
    Console.Write(item.Choices[0].Delta?.Content);
}

Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("----------------------------------");
Console.WriteLine("----------------------------------");
Console.WriteLine("");

ChatHistory chatHistory =
[
    new ChatMessageContent()
    {
        Role = AuthorRole.User,
        Content = "Hello, how are you?"
    },

    new ChatMessageContent()
    {
        Role = AuthorRole.Assistant,
        Content = "I am good, how are you?"
    },

    new ChatMessageContent()
    {
        Role = AuthorRole.User,
        Content = "What is your name?"
    }
];
// Create a sk chat completion service
var builder = Kernel.CreateBuilder()
    .AddDeepSeekChatCompletion(apiKey, modelId, endpoint);
var kernel = builder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

Console.WriteLine("Create a sk chat completion service");
Console.WriteLine("😀User >> "+ chatHistory.Last().Content);
var chatMessage2 = await chatCompletionService.GetChatMessageContentsAsync(chatHistory);
Console.WriteLine("👨Assistant >> "+ chatMessage2.Last().Content);

Console.WriteLine("Create a sk streaming chat completion service");
Console.WriteLine("😀User >> "+ chatHistory.Last().Content);
var streamingChatMessage2 = chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory);

Console.Write("👨Assistant >> ");
await foreach (var item in streamingChatMessage2)
{
    Thread.Sleep(200);
    Console.Write(item.Content);
}

Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("----------------------------------");
Console.WriteLine("----------------------------------");
Console.WriteLine("");

Console.WriteLine("Create a sk chat completion service with tools");

var openAIClientCredential = new ApiKeyCredential(apiKey);
var openAIClientOption = new OpenAIClientOptions
{
    Endpoint = new Uri("https://api.deepseek.com"),

};
var builder1 = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(modelId, new OpenAIClient(openAIClientCredential, openAIClientOption));

var kernel1 = builder1.Build();
var chatCompletionService1 = kernel1.GetRequiredService<IChatCompletionService>();

Console.WriteLine("😀User >> "+ chatHistory.Last().Content);
var result = chatCompletionService1.GetStreamingChatMessageContentsAsync(
    chatHistory
);
Console.Write("👨Assistant >> ");
await foreach (var item in result)
{
    Thread.Sleep(200);
    Console.Write(item.Content);
}