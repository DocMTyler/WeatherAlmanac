using System;
using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.Core.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace WeatherAlmanac.BLL
{
    public class RecordService : IRecordService
    {
        private IRecordRepository _repo;

        public RecordService(IRecordRepository repo)
        {
            _repo = repo;
        }

        public Result<List<DateRecord>> LoadRange(DateTime start, DateTime end)
        {
            Result<List<DateRecord>> result = new();
            List<DateRecord> records = new List<DateRecord>();
            result.Data = records;
            foreach(var record in _repo.GetAll().Data)
            {
                if(record.Date >= start && record.Date <= end)
                {
                    result.Data.Add(record);
                }
            }
            
            result.Success = !string.IsNullOrEmpty(result.Data.ToString());
            result.Message = result.Success ? "Range loaded" : "Range not loaded";
            return result;
        }

        public Result<DateRecord> Get(DateTime date)
        {
            bool found = false;
            Result<DateRecord> result = new();
            DateRecord newRecord = new DateRecord();
            result.Data = newRecord;

            foreach (var record in _repo.GetAll().Data)
            {
                if (record.Date == date)
                {
                    found = true;
                    result.Data = record;
                    break;
                }
            }

            result.Success = found;
            result.Message = result.Success ? "Got" : "not got";
            return result;
        }

        public Result<List<DateRecord>> AutoAddRecords(string path)
        {
            Result<List<DateRecord>> result = new Result<List<DateRecord>>();
            result.Data = new List<DateRecord>();
            
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string currentLine = sr.ReadLine();
                    currentLine = sr.ReadLine();
                    int lineCount = 0;

                    while (currentLine != null)
                    {
                        lineCount++;
                        DateRecord record = new DateRecord();
                        string[] columns = currentLine.Split(",");

                        record.Date = DateTime.Parse(columns[0]);
                        record.HighTemp = decimal.Parse(columns[1]);
                        record.LowTemp = decimal.Parse(columns[2]);
                        record.Humidity = decimal.Parse(columns[3]);
                        record.Description = columns[4];

                        var service = _repo.Add(record);
                        result.Data.Add(record);
                        currentLine = sr.ReadLine();
                    }
                }
            }
            else
            {
                Console.WriteLine($"File at {path} not found.");
            }
            result.Success = !String.IsNullOrEmpty(result.Data.ToString());
            result.Message = result.Success ? $"{result.Data.Count} records added." : "Records not added";

            return result;
        }

        public Result<DateRecord> Add(DateRecord record)
        {
            return _repo.Add(record);
        }

        public Result<DateRecord> Remove(DateTime date)
        {
            return _repo.Remove(date);
        }

        public Result<DateRecord> Edit(DateRecord record)
        {
            return _repo.Edit(record);
        }
    }
}
