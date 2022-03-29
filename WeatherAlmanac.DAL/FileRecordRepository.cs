using System;
using System.Collections.Generic;
using WeatherAlmanac.Core.Interfaces;
using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.Core.Exceptions;

namespace WeatherAlmanac.DAL
{
    public class FileRecordRepository : IRecordRepository, ILogger
    {
        private List<DateRecord> _records;

        public FileRecordRepository(DateRecord record)
        {
            _records = new List<DateRecord>();
            DateRecord fileRecord = new DateRecord();
            fileRecord.Date = record.Date;
            fileRecord.HighTemp = record.HighTemp;
            fileRecord.LowTemp = record.LowTemp;
            fileRecord.Humidity = record.Humidity;
            fileRecord.Description = "";
            _records.Add(fileRecord);
        }

        public Result<List<DateRecord>> GetAll()
        {
            Result<List<DateRecord>> result = new Result<List<DateRecord>>();
            result.Success = true;
            result.Message = "";
            result.Data = new List<DateRecord>(_records);
            return result;
        }

        public Result<DateRecord> Add(DateRecord record)
        {
            int beforeAdd = _records.Count;
            _records.Add(record);
            int afterAdd = _records.Count;

            Result<DateRecord> result = new Result<DateRecord>();
            result.Success = beforeAdd != afterAdd;
            result.Message = result.Success ? "Record was added" : "Record was not added";
            result.Data = record;

            return result;
        }

        public Result<DateRecord> Remove(DateTime date)
        {
            int beforeRemove = _records.Count;

            DateRecord recordToRemove = new();
            foreach (var record in _records)
            {
                if (record.Date == date)
                {
                    recordToRemove = record;
                }
            }
            _records.Remove(recordToRemove);

            int afterRemove = _records.Count;
            Result<DateRecord> result = new();
            result.Success = beforeRemove != afterRemove;
            result.Message = result.Success ? "Record was removed" : "Record was not removed";
            result.Data = recordToRemove;

            return result;
        }

        public Result<DateRecord> Edit(DateRecord record)
        {
            int indexToEdit = 0;
            for (int i = 0; i < _records.Count; i++)
            {
                if (_records[i].Date == record.Date && _records[i].HighTemp == record.HighTemp &&
                    _records[i].LowTemp == record.LowTemp && _records[i].Humidity == record.Humidity)
                {
                    indexToEdit = i;
                }
            }
            var recordBeforeEdit = _records[indexToEdit];
            _records[indexToEdit] = record;

            Result<DateRecord> result = new();
            result.Success = recordBeforeEdit != _records[indexToEdit];
            result.Message = result.Success ? "Record was edited" : "Record was not edited";
            result.Data = record;

            return result;
        }

        public void Log(string message)
        {
            try
            {
                
            }
            catch(Exception e)
            {
                Log(message);
                Console.WriteLine(e.Message);
            }
        }
    }
}
