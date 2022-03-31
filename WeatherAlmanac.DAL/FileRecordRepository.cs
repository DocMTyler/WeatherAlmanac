using System;
using System.Collections.Generic;
using WeatherAlmanac.Core.Interfaces;
using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.Core.Exceptions;
using System.IO;
using System.Linq;

namespace WeatherAlmanac.DAL
{
    public class FileRecordRepository : IRecordRepository
    {
        private readonly List<DateRecord> _records;
        private readonly string _fileName;
        private readonly ILogger _logger;

        public FileRecordRepository(string fileName, ILogger logger)
        {
            _fileName = fileName;
            _logger = logger;

            _records = new List<DateRecord>();
            Init();
        }


        private void Init()
        {
            try
            {
                if (!File.Exists(_fileName))
                {
                    File.Create(_fileName).Close();
                    return;
                }

                using var sr = new StreamReader(_fileName);
                string? row;

                // Skip the header line
                sr.ReadLine();

                while ((row = sr.ReadLine()) != null)
                {
                    _records.Add(Deserialize(row));
                }
            }
            catch(Exception e)
            {
                _logger.Log(e.Message);
            }


        }

        private static DateRecord Deserialize(string row)
        {
            var record = new DateRecord();
            var values = row.Split(',');
            record.Date = DateTime.Parse(values[0]);
            record.HighTemp = int.Parse(values[1]);
            record.LowTemp = int.Parse(values[2]);
            record.Humidity = int.Parse(values[3]);

            // Keep the Description at the end so that extra commas won't matter. Skip first 4 and join the rest.
            record.Description = String.Join(", ", values.Skip(4));

            return record;
        }

        public Result<List<DateRecord>> GetAll()
        {
            Result<List<DateRecord>> result = new Result<List<DateRecord>>();
            try
            {
                result.Success = true;
                result.Message = "";
                result.Data = new List<DateRecord>(_records);
                return result;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
            return result;
        }

        public Result<DateRecord> Add(DateRecord record)
        {
            Result<DateRecord> result = new Result<DateRecord>();
            try
            {
                int beforeAdd = _records.Count;
                _records.Add(record);
                int afterAdd = _records.Count;
                
                result.Success = beforeAdd != afterAdd;
                result.Message = result.Success ? "Record was added" : "Record was not added";
                result.Data = record;

                return result;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
            return result;
        }

        public Result<DateRecord> Remove(DateTime date)
        {
            Result<DateRecord> result = new();
            try
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

                result.Success = beforeRemove != afterRemove;
                result.Message = result.Success ? "Record was removed" : "Record was not removed";
                result.Data = recordToRemove;

                return result;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
            return result;
        }

        public Result<DateRecord> Edit(DateRecord record)
        {
            Result<DateRecord> result = new();
            try
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


                result.Success = recordBeforeEdit != _records[indexToEdit];
                result.Message = result.Success ? "Record was edited" : "Record was not edited";
                result.Data = record;

                return result;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
            return result;
        }
    }
}
