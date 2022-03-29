using System;

namespace WeatherAlmanac.Core.Exceptions
{
    public class NoRecordsFoundException : Exception
    {
        public NoRecordsFoundException(string message) : base(message)
        {

        }
    }
}
