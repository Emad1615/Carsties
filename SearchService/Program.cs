using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.RequestHelpers;
using SearchService.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(x => x.AddMaps(typeof(MappingProfile).Assembly));
var root = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHttpClient<AuctionServiceHttpClient>()
    .AddPolicyHandler(GetRetryPolicy())
    .AddPolicyHandler(GetCircuitBreakerPolicy());

var app = builder.Build();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Database initialized and migration
//try
//{
//    await DbInitializer.DbInit(app);
//    Console.WriteLine("Database initialized successfully.");
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"An error occurred during database initialization: {ex.Message}");
//}
#endregion

app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        Console.WriteLine("Application has started.");

        await Policy.Handle<TimeoutException>()
                    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(10))
                    .ExecuteAndCaptureAsync(async () => await DbInitializer.DbInit(app));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during application startup: {ex.Message}");
    }
});

app.Run();

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    => HttpPolicyExtensions.HandleTransientHttpError()
    .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
    .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
#region WaitAndRetryAsync
//.WaitAndRetryAsync(
//    retryCount: 3,
//     sleepDurationProvider:retryAttem=> TimeSpan.FromMilliseconds(200* Math.Pow(2,retryAttem))
//    );
#endregion

static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
    HttpPolicyExtensions.HandleTransientHttpError()
    .CircuitBreakerAsync(
            handledEventsAllowedBeforeBreaking: 5,
            durationOfBreak: TimeSpan.FromSeconds(30)
        );


