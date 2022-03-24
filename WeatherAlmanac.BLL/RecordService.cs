using System;
using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.Core.Interfaces;
using System.Collections.Generic;

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
