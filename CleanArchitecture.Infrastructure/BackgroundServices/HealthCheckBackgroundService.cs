using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.BackgroundServices;
internal sealed class HealthCheckBackgroundService : BackgroundService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HealthCheckBackgroundService> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _healthCheckUrl;
    private int _intervalMinutes;

    public HealthCheckBackgroundService(
        IHttpClientFactory httpClientFactory,
        ILogger<HealthCheckBackgroundService> logger,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _configuration = configuration;
        _healthCheckUrl = _configuration["HealthCheck:Url"]!;
        _intervalMinutes = int.TryParse(_configuration["HealthCheck:IntervalMinutes"], out var interval) ? interval : 5;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Running health check at : {time}", DateTimeOffset.Now);

            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = null;


            try
            {
                response = await client.GetAsync(_healthCheckUrl, stoppingToken);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Health check successful at: {time}", DateTimeOffset.Now);
                }
                else
                {
                    _logger.LogWarning("Health check failed at: {time}", DateTimeOffset.Now);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Health check error at :{time}", DateTimeOffset.Now);
            }

            await Task.Delay(TimeSpan.FromMinutes(_intervalMinutes), stoppingToken);
        }
    }
}
