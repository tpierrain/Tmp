namespace TrainTrain.Api;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        return ConfigureServices<SwaggerServicesConfiguration>(services);
    }

    private static IServiceCollection ConfigureServices<TServicesConfiguration>(IServiceCollection services, TServicesConfiguration strategy)
        where TServicesConfiguration : IServicesConfiguration
    {
        strategy.ConfigureServices(services);
        return services;
    }
}

public interface IServicesConfiguration
{
    void ConfigureServices(IServiceCollection services);
}

public sealed class SwaggerServicesConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddOptions<MobileBffSwaggerOptions>()
            .BindConfiguration("Swagger")
            .ValidateDataAnnotations()
            .Services
            .AddSwaggerGen();
    }
}