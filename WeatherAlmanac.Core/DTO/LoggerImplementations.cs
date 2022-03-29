using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAlmanac.Core.Interfaces;

namespace WeatherAlmanac.Core.DTO
{
    public class NullLogger : ILogger
    {
        public void Log(string message)
        {
            return;
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FileLogger : ILogger
    {
        private string _filePath;
        private DateTime timeStamp = DateTime.Now;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            using (StreamWriter sw = new StreamWriter(message, true))
            {
                sw.WriteLine($"{timeStamp}, {message}");
            }
        }
    }
}
