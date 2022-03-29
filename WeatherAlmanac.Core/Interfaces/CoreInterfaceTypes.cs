using System;
using System.Collections.Generic;
using WeatherAlmanac.Core.DTO;

namespace WeatherAlmanac.Core.Interfaces
{
    #region Interface Types
    public interface IRecordRepository
    {
        Result<List<DateRecord>> GetAll();          //Retrieves all records from storage
        Result<DateRecord> Add(DateRecord record);  //Adds a record to storage
        Result<DateRecord> Remove(DateTime date);   //Removes a record for date
        Result<DateRecord> Edit(DateRecord record); //Replaces a record with the same date
    }

    public interface IRecordService
    {
        Result<List<DateRecord>> LoadRange(DateTime start, DateTime end); //Retrieves only records within range
        Result<DateRecord> Get(DateTime date);                            //Get only the record with the specified date
        Result<DateRecord> Add(DateRecord record);                        //Adds a record to storage
        Result<DateRecord> Remove(DateTime date);                         //Removes record for date
        Result<DateRecord> Edit(DateRecord record);                       //Replaces a record with the same date
        Result<List<DateRecord>> AutoAddRecords(string path);                  //Automatically adds records from a file
    }
    
    public interface ILogger
    {
        void Log(string message);
    }
    
    #endregion
}
