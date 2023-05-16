using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope =host.Services.CreateScope())
            {
                var services=scope.ServiceProvider;
                var congiguration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try {
                    logger.LogInformation("migartion postgre sql");
                    using var connection = new NpgsqlConnection(congiguration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };
                    command.CommandText = "Drop table if exists coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"create table coupon(id serial primary key, ProductName varchar(20) not null , description text , amount int)";
                    command.ExecuteNonQuery();

                    command.CommandText = "insert into coupon(ProductName, description, amount) values ('IPhone X', 'IPhone discount', 100)";
                    command.ExecuteNonQuery();

                    command.CommandText = "insert into coupon(ProductName, description, amount) values ('Samsung 10', 'Samsung discount', 120)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("migrate postgre sql database");
                }
                catch(NpgsqlException ex) {
                    logger.LogError(ex, "while migrating the postgre  sql");

                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }
            return host;
        }
    }
}
