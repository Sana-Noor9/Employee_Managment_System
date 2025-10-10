
public class DailyAttendanceHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DailyAttendanceHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRunTime = DateTime.Today.AddHours(8); // 8:00 AM

            if (now > nextRunTime)
                nextRunTime = nextRunTime.AddDays(1);

            var delay = nextRunTime - now;
            await Task.Delay(delay, stoppingToken); // wait until 8 AM

            using var scope = _serviceProvider.CreateScope();
            var attendanceService = scope.ServiceProvider.GetRequiredService<DailyAttendanceService>();
            await attendanceService.SeedDailyAttendanceAsync();

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // next day
        }
    }
}
