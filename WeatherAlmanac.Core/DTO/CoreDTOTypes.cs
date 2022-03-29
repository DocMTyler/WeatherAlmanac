using System;
using System.Text;

namespace WeatherAlmanac.Core.DTO
{
    #region DTO Types
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class DateRecord
    {
        public DateTime Date { get; set; }
        public decimal HighTemp { get; set; }
        public decimal LowTemp { get; set; }
        public decimal Humidity { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("");
            sb.AppendLine("---------------------");
            sb.AppendLine(Date.ToString("D"));
            sb.AppendLine($"High: {HighTemp} F");
            sb.AppendLine($"Low: {LowTemp} F");
            sb.AppendLine($"Humidity: {Humidity}%");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine("---------------------");

            return sb.ToString();
        }
    }

    public enum ApplicationMode
    {
        LIVE,
        TEST
    }

    public enum LoggingMode
    {
        None = 1, 
        File,
        Console
    }

    #endregion
}
