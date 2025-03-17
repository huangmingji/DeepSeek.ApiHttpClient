using DeepSeek.ApiHttpClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Stargazer.Common.Extend;

namespace DeepSeek.ApiHttpClient;

public static class DeepSeekKernelBuilderExtensions
{
    public static IKernelBuilder AddDeepSeekChatCompletion(
        this IKernelBuilder builder,
        string? apiKey = null,
        string? modelId = null,
        string? endpoint = null,
        string? serviceId = null)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.AddDeepSeekChatCompletion(apiKey, modelId, endpoint, serviceId);
        return builder;
    }

    public static IServiceCollection AddDeepSeekChatCompletion(
        this IServiceCollection services,
        string? apiKey = null,
        string? modelId = null,
        string? endpoint = null,
        string? serviceId = null)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var builder = DeepSeekClientBuilder.Build();
        if (apiKey != null)
        {
            builder.SetApiKey(apiKey);
        }

        if (modelId != null)
        {
            builder.SetModel(modelId);
        }

        if (endpoint != null)
        {
            builder.SetEndpoint(endpoint);
        }

        return services.AddKeyedSingleton<IChatCompletionService>(serviceId, new DeepSeekChatCompletionService
        {
            DeepSeekClient = builder.CreateDeepSeekClient()
        });
    }
}