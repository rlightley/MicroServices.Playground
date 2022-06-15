namespace MicroServices.Playground.ServiceTwo.Api.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddCustomEvents(this IServiceCollection services, IConfiguration config)
    {
        services.AddCap(options =>
        {
            options.UseDashboard(dashboardOptions =>
            {
                dashboardOptions.PathMatch = $"{config.GetValue("PathBase", string.Empty)}/cap";
            });
            options.UseSqlServer(opt =>
            {
                opt.ConnectionString = config["ConnectionStrings:DefaultConnection"];
            });

            if (config.GetValue<bool>("EventBus:AzureServiceBusEnabled"))
            {
                options.UseAzureServiceBus(opt =>
                {
                    opt.ConnectionString = config["EventBus:Connection"];
                    opt.TopicPath = config.GetValue<string>("EventBus:AzureServiceBusTopicName");
                });
            }
            else
            {
                options.UseRabbitMQ(c =>
                {
                    c.HostName = config["EventBus:Connection"];
                    c.UserName = config.GetValue("EventBus:UserName", "guest");
                    c.Password = config.GetValue("EventBus:Password", "guest");
                    c.Port = 5672;
                });
            }

            options.FailedRetryCount = config.GetValue("EventBus:RetryCount", 5);
            options.DefaultGroupName = config["EventBus:SubscriberName"];
        });
    }

    public static void AddEventHandlers(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<Program>()
            .AddClasses(classes => classes.AssignableTo(typeof(ICapSubscribe)))
            .AsSelf()
            .WithTransientLifetime());
    }
}