using NUnit.Framework;
using System.IO;
using WeatherAlmanac.DAL;
using WeatherAlmanac.Core.DTO;
using System;
using System.Collections.Generic;

namespace WeatherAlmanac.Tests.DAL
{
    public class RecordRepoTests
    {
        [SetUp]
        public void Setup()
        {
            // Delete any Test Data File
            if (File.Exists(TestDataFile)) File.Delete(TestDataFile);

            // Copy the seed contents to a newly created Test Data File
            File.Copy(SeedFile, TestDataFile);

            //_repo = new FileRecordRepository("", ); 
        }

        private const string SeedFile = "../../../DAL/test_data/test.seed.csv";
        private const string TestDataFile = "../../../DAL/test_data/test.data.csv";

        private FileRecordRepository? _repo;

        [Test]
        public void CanCreateTestFile()
        {
            Assert.IsTrue(File.Exists(TestDataFile));
        }

        [Test]
        public void GetAll_Returns3RecordsWithFirstHighTempOf32()
        {
            var records = _repo.GetAll();

            Assert.AreEqual(3, records.Data.Count);
            Assert.AreEqual(32, records.Data[0].HighTemp);
        }

        [Test]
        public void Add_AddsRecordsToFileWithHighTempOf32()
        {
            var record = new DateRecord() 
            {
               Date = DateTime.Now, HighTemp = 42, LowTemp = 19, Humidity = 5
            };

            _repo.Add(record);

            var records = _repo.GetAll();
            Assert.AreEqual(42, records.Data[3].HighTemp);
        }

        //Test Update and Delete
    }
}