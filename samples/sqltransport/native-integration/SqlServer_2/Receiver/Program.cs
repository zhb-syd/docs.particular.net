﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        Console.Title = "Samples.SqlServer.NativeIntegration";
        #region EndpointConfiguration
        var busConfiguration = new BusConfiguration();
        var transport = busConfiguration.UseTransport<SqlServerTransport>();
        transport.ConnectionString(@"Data Source=.\SqlExpress;Database=samples;Integrated Security=True");
        busConfiguration.EndpointName("Samples.SqlServer.NativeIntegration");
        busConfiguration.UseSerialization<JsonSerializer>();
        #endregion
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.EnableInstallers();

        using (Bus.Create(busConfiguration).Start())
        {
            Console.WriteLine("Press enter to send a message");
            Console.WriteLine("Press any other key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }
                await PlaceOrder()
                    .ConfigureAwait(false);
            }

        }
    }

    static async Task PlaceOrder()
    {
        #region MessagePayload

        var message = @"{
                           $type: 'PlaceOrder',
                           OrderId: 'Order from ADO.net sender'
                        }";

        #endregion

        #region SendingUsingAdoNet

        var connectionString = @"Data Source=.\SqlExpress;Database=samples;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync()
                .ConfigureAwait(false);
            var insertSql = "INSERT INTO [Samples.SqlServer.NativeIntegration] ([Id],[Recoverable],[Headers],[Body]) VALUES (@Id,@Recoverable,@Headers,@Body)";
            using (var command = new SqlCommand(insertSql, connection))
            {
                command.CommandType = CommandType.Text;
                var parameters = command.Parameters;
                parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                parameters.Add("Headers", SqlDbType.VarChar).Value = "";
                parameters.Add("Body", SqlDbType.VarBinary).Value = Encoding.UTF8.GetBytes(message);
                parameters.Add("Recoverable", SqlDbType.Bit).Value = true;
                await command.ExecuteNonQueryAsync()
                    .ConfigureAwait(false);
            }
        }

        #endregion
    }
}
