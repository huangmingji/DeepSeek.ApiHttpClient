using System.Runtime.CompilerServices;
using DeepSeek.ApiHttpClient.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace DeepSeek.ApiHttpClient;

public class DeepSeekChatCompletionService : IChatCompletionService
{
    public IReadOnlyDictionary<string, object?> Attributes { get; }

    public required DeepSeekClient DeepSeekClient { get; init; }

    public async Task<IReadOnlyList<ChatMessageContent>> GetChatMessageContentsAsync(ChatHistory chatHistory, PromptExecutionSettings? executionSettings = null,
        Kernel? kernel = null, CancellationToken cancellationToken = new CancellationToken())
    {
        List<ChatMessageHistory> chatMessageHistories = new List<ChatMessageHistory>();
        foreach (var chatMessage in chatHistory)
        {
            chatMessageHistories.Add(new ChatMessageHistory()
            {
                Role = chatMessage.Role.ToString(),
                Content = chatMessage.Content??""
            });
        }

        var response = await DeepSeekClient.SetChatMessageHistory(chatMessageHistories)
            .GetChatMessageContentsAsync(cancellationToken);
        List<ChatMessageContent> chatMessageContents = new List<ChatMessageContent>();
        foreach (var choice in response.Choices)
        {
            chatMessageContents.Add(new ChatMessageContent()
            {
                Content = choice.Message.Content,
                Role = new AuthorRole(choice.Message.Role)
            });
        }
        return chatMessageContents;
    }

    public async IAsyncEnumerable<StreamingChatMessageContent> GetStreamingChatMessageContentsAsync(
        ChatHistory chatHistory,
        PromptExecutionSettings? executionSettings = null, Kernel? kernel = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = new CancellationToken())
    {
        List<ChatMessageHistory> chatMessageHistories = new List<ChatMessageHistory>();
        foreach (var chatMessage in chatHistory)
        {
            chatMessageHistories.Add(new ChatMessageHistory()
            {
                Role = chatMessage.Role.ToString(),
                Content = chatMessage.Content ?? ""
            });
        }

        var response = DeepSeekClient.SetChatMessageHistory(chatMessageHistories)
            .GetStreamingChatMessageContentsAsync(cancellationToken);
        await foreach (var item in response)
        {
            yield return new StreamingChatMessageContent(AuthorRole.Assistant, item.Choices.First().Delta?.Content);
        }
    }
}