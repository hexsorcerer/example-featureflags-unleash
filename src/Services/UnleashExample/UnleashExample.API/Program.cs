using Unleash;
using Unleash.ClientFactory;

var builder = WebApplication.CreateBuilder(args);

var settings = new UnleashSettings
{
    AppName = "unleash-example",
    ProjectId = "Default",
    UnleashApi = new Uri(@"http://unleash-server:4242/api/"),
    CustomHttpHeaders = new Dictionary<string, string>
    {
        {
            "Authorization",
            $@"{Environment.GetEnvironmentVariable("UNLEASH_API_TOKEN") ?? string.Empty}"
        }
    }
};

var unleashFactory = new UnleashClientFactory();

var unleash = await unleashFactory
    .CreateClientAsync(settings, synchronousInitialization: true)
    .ConfigureAwait(false);

unleash.ConfigureEvents(cfg =>
{
    cfg.ImpressionEvent = evt => { Console.WriteLine($"{evt.FeatureName}: {evt.Enabled}"); };
});

builder.Services.AddSingleton(unleash);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
