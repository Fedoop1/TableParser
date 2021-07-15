using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CreateTableOfRecordsTypeOfT
{
    public static class DataParser
    {
        private static readonly Type[] ValidPropertiesTypes = new Type[] { typeof(DateTime), typeof(int), typeof(string), typeof(char) };
        private static readonly Type[] LeftPaddingTypes = new Type[] { typeof(int), typeof(DateTime) };

        public static void ParseToFile<T>(IEnumerable<T> source, string path)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "Source is null");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Destination file path can't be null or empty or whitespace");
            }

            var parsedData = new List<string>(); 

            foreach (var item in source)
            {
                parsedData.Add(ParseData(item));
            }

            WriteToFile(parsedData, typeof(T), path);
        }

        private static string ParseData<T>(T data)
        {
            var sBuilder = new StringBuilder();

            foreach (var property in typeof(T).GetProperties())
            {
                if (ValidPropertiesTypes.Contains(property.PropertyType))
                {
                    if (property.PropertyType == typeof(DateTime))
                    {
                        sBuilder.Append($"{DateTime.Parse(property.GetValue(data).ToString(), CultureInfo.InvariantCulture).Year},");
                        continue;
                    }

                    sBuilder.Append($"{property.GetValue(data) ?? "empty"},");
                }
            }

            return sBuilder.ToString().TrimEnd(',');
        }

        private static void WriteToFile(IEnumerable<string> data, Type dataType, string path)
        {
            var propertiesInfo = FindMaximumDataLength(data, ParseProperties(dataType));
            var tableWidth = Math.Max(propertiesInfo.Sum(x => x.propertyName.Length), propertiesInfo.Sum(x => x.maxLength)) + (3 * propertiesInfo.Length) + 1;

            using var destinationStreamWriter = new StreamWriter(path, false, Encoding.UTF8);
            destinationStreamWriter.AutoFlush = true;

            WriteFileHeader(destinationStreamWriter, tableWidth, propertiesInfo);
            WriteFileBody(destinationStreamWriter, tableWidth, data, propertiesInfo);
        }

        private static PropertyInfo[] ParseProperties(Type dataType)
        {
            var propertyNames = new List<PropertyInfo>();

            foreach (var property in dataType.GetProperties())
            {
                if (ValidPropertiesTypes.Contains(property.PropertyType))
                {
                    propertyNames.Add(property);
                }
            }

            return propertyNames.ToArray();
        }

        private static void WriteFileHeader(StreamWriter writer, int tableWidth, (string propertyName, bool paddingLeft, int maxLength)[] propertiesInfo)
        {
            writer.WriteLine(new string('-', tableWidth));

            writer.Write('|');

            for (int propertyIndex = 0; propertyIndex < propertiesInfo.Length; propertyIndex++)
            {
                var propertyData = propertiesInfo[propertyIndex];
                writer.Write($" {propertyData.propertyName.PadLeft(propertyData.maxLength)} |");
            }

            writer.Write('\n');
            writer.WriteLine(new string('-', tableWidth));
        }

        private static void WriteFileBody(StreamWriter writer, int tableWidth, IEnumerable<string> data, (string propertyName, bool paddingLeft, int maxLength)[] propertiesInfo)
        {
            foreach (var record in data)
            {
                var recordData = record.Split(',');
                writer.Write('|');

                for (int propertyIndex = 0; propertyIndex < propertiesInfo.Length; propertyIndex++)
                {
                    if (propertiesInfo[propertyIndex].paddingLeft)
                    {
                        writer.Write($" {recordData[propertyIndex].PadLeft(propertiesInfo[propertyIndex].maxLength)} |");
                        continue;
                    }

                    writer.Write($" {recordData[propertyIndex].PadRight(propertiesInfo[propertyIndex].maxLength)} |");
                }

                writer.Write('\n');
                writer.WriteLine(new string('-', tableWidth));
            }
        }

        private static (string propertyName, bool paddingLeft, int maxLength)[] FindMaximumDataLength(IEnumerable<string> data, PropertyInfo[] properties)
        {
            var result = new List<(string propertyName, bool paddingLeft, int maxLength)>();

            for (int propertyIndex = 0; propertyIndex < properties.Length; propertyIndex++)
            {
                int maxLength = data.Select(record => record.Split(',')).Select(x => x[propertyIndex].Length).Max();
                result.Add((properties[propertyIndex].Name, LeftPaddingTypes.Contains(properties[propertyIndex].PropertyType), Math.Max(maxLength, properties[propertyIndex].Name.Length)));
            }

            return result.ToArray();
        }
    }
}
