using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Stargazer.Common.Extend;

namespace DeepSeek.ApiHttpClient;

public static class DeepSeekKernelBuilderExtensions
{
    public static DeepSeekConfig Config { get; set; } = new DeepSeekConfig();

    public static IServiceCollection UseDeepSeek(this IServiceCollection serviceCollection, DeepSeekConfig config)
    {
        Config = config;
        return serviceCollection;
    }

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
        
        if (apiKey != null)
        {
            Config.ApiKey = apiKey;
        }

        if (modelId != null)
        {
            Config.Model = modelId;
        }

        if (endpoint != null)
        {
            Config.Endpoint = endpoint;
        }

        return services.AddKeyedSingleton<IChatCompletionService>(serviceId, new DeepSeekChatCompletionService());
    }
}