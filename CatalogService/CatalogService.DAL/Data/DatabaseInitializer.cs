using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CatalogService.DAL.Data
{
    internal class DatabaseInitializer : IHostedService
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
                await CreatePublicationAsync(dbContext, cancellationToken);
            }
        }

        private async Task CreatePublicationAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
        {
                await using var connection = dbContext.Database.GetDbConnection() as Npgsql.NpgsqlConnection;
                if (connection == null) throw new InvalidOperationException("Failed to get NpgsqlConnection");

                if (connection.State != System.Data.ConnectionState.Open)
                    await connection.OpenAsync(cancellationToken);

            {
                await using var command = connection.CreateCommand();
                command.CommandText = $@"
                    DROP PUBLICATION IF EXISTS {_configuration["PgPublication:Name"]};
                    CREATE PUBLICATION {_configuration["PgPublication:Name"]} FOR ALL TABLES;";

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
            {
                await using var command = connection.CreateCommand();
                command.CommandText = $@"SELECT * FROM pg_create_logical_replication_slot('my_slot', 'pgoutput');";

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
            {
                await using var command = connection.CreateCommand();
                command.CommandText = $@"SELECT * FROM pg_create_logical_replication_slot('my_slot_2', 'pgoutput');";

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
            {
                await using var command = connection.CreateCommand();
                command.CommandText = $@"SELECT * FROM pg_create_logical_replication_slot('my_slot_3', 'pgoutput');";

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}