using CartService.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public DatabaseInitializer(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var created = await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        if (created)
        {
            await CreateSubscriptionAsync(dbContext, cancellationToken);
        }
    }

    private async Task CreateSubscriptionAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        await using var connection = dbContext.Database.GetDbConnection() as Npgsql.NpgsqlConnection;
        if (connection == null) throw new InvalidOperationException("Failed to get NpgsqlConnection");

        if (connection.State != System.Data.ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = $@"
                    DROP SUBSCRIPTION IF EXISTS {_configuration["PgSubscription:Name"]};
                    CREATE SUBSCRIPTION {_configuration["PgSubscription:Name"]} CONNECTION '{_configuration["PgSubscription:Connection"]}' 
                        PUBLICATION {_configuration["PgSubscription:PublicationName"]}
                        WITH (create_slot = false, slot_name = 'my_slot');";


        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}