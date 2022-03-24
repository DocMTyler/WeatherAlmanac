using System;
using WeatherAlmanac.Core.Interfaces;
using WeatherAlmanac.Core.DTO;
using System.Collections.Generic;

namespace WeatherAlmanac.DAL
{
    public class MockRecordRepository : IRecordRepository
    {
        private List<DateRecord> _records; 
        public MockRecordRepository()
        {
            _records = new List<DateRecord>();
            DateRecord bogus = new DateRecord();
            bogus.Date = new DateTime();
            bogus.HighTemp = 82;
            bogus.LowTemp = 40;
            bogus.Humidity = .30m;
            bogus.Description = "Really inconsistent weather today";
            _records.Add(bogus);
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
            if(!_records.Contains(record)) _records.Add(record);
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
                if(record.Date == date)
                {
                    recordToRemove = record;
                }
            }
            if(_records.Contains(recordToRemove)) _records.Remove(recordToRemove);
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
            for(int i = 0; i < _records.Count; i++)
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
    }
}
