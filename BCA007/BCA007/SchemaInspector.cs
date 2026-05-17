
using System;
using System.Linq;
using System.Threading.Tasks;
using BCA007.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class SchemaInspector
{
    public static async Task Inspect(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        string[] tables = { "T_Student_Fee", "T_Staff_Payment", "T_Payment" };

        foreach (var table in tables)
        {
            Console.WriteLine($"--- Schema for {table} ---");
            try
            {
                var cmd = context.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = $"SELECT COLUMN_NAME, DATA_TYPE, NUMERIC_PRECISION, NUMERIC_SCALE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'";
                context.Database.OpenConnection();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var col = reader["COLUMN_NAME"];
                    var type = reader["DATA_TYPE"];
                    var prec = reader["NUMERIC_PRECISION"];
                    var scale = reader["NUMERIC_SCALE"];
                    Console.WriteLine($"{col}: {type} ({prec},{scale})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inspecting {table}: {ex.Message}");
            }
        }
    }
}
