using System.Reflection;
using bpoLabs.Common.Extensions;
using bpoLabs.DbContext;

namespace bpoLabs.Common.Database
{
    public interface IDatabaseHelper
    {
        Task BulkInsert<T>(string tableName, IEnumerable<T> records);
    }

    public class DatabaseHelper : IDatabaseHelper
    {

        private readonly IDapperWrapper _dapperWrapper;
        private readonly DapperDbContext _dbContext;

        public DatabaseHelper(IDapperWrapper dapperWrapper, DapperDbContext dbContext)
        {
            _dapperWrapper = dapperWrapper;
            _dbContext = dbContext;
        }

        public async Task BulkInsert<T>(string tableName, IEnumerable<T> records)
        {
            var type = typeof(T);
            var fieldsList = type.GetProperties();
            var partitions = records.ToList().Partition(1000);
            foreach (var sql in partitions.Select(partition => GenerateSql(fieldsList, tableName, partition)))
            {
                using var connection = _dbContext.CreateConnection();
                await _dapperWrapper.ExecuteAsync(connection, sql);
            }

        }

        public static string GenerateSql<T>(PropertyInfo[] fieldsList, string tableName, ICollection<T> records)
        {
            var tableFields = fieldsList.Select(x => new TableField { Name = x.Name, PropertyType = x.PropertyType }).ToList();
            var insertSection = GenerateInsertSection(tableName);
            var fieldsSection = GenerateFieldsSection(tableFields);
            var valueSection = GenerateValuesSection<T>(tableFields, records);
            var sql = insertSection + " " + fieldsSection + " values " + valueSection;
            return sql;
        }

        public static string GenerateFieldsSection(ICollection<TableField> fields)
        {
            return $"({(string.Join(",", fields.Select(x => x.Name).ToList()))})";
        }

        public static string GenerateInsertSection(string tableName)
        {
            return $"INSERT INTO {tableName} ";
        }

        public static string GenerateValuesSection<T>(ICollection<TableField> fields, ICollection<T> records)
        {

            var statementList = records
                .Select(record =>
                    (from field in fields
                     let value = record?.GetType().GetProperty(field.Name)?.GetValue(record)
                     select GetDatabaseFormattedValue(field, value?.ToString() ?? string.Empty)).ToList())
                .Select(valueList => $"({string.Join(",", valueList)})").ToList();
            return string.Join(",", statementList);

        }

        private static string? GetDatabaseFormattedValue(TableField field, object value)
        {
            if (field.PropertyType == typeof(string))
            {
                return $"'{value}'";
            }
            if (field.PropertyType == typeof(int))
            {
                return value.ToString() ?? throw new InvalidOperationException();
            }
            if (field.PropertyType == typeof(DateTime))
            {
                var receivedValue = DateTime.Parse(value.ToString() ?? string.Empty);
                var returValue = receivedValue.ToString("yyyy-MM-dd hh:mm:ss");
                return $"CONVERT(datetime, '{returValue}', 101)";
            }
            return value.ToString();
        }
    }

    public sealed class TableField
    {
        public string Name { get; set; }
        public Type PropertyType { get; set; }
    }


}
