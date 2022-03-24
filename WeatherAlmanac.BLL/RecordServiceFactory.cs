using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAlmanac.Core.Interfaces;
using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.DAL;

namespace WeatherAlmanac.BLL
{
    public class RecordServiceFactory 
    {
        public static IRecordService GetRecordService(ApplicationMode mode)
        {
            if(mode == ApplicationMode.TEST)
            {
                return new RecordService(new MockRecordRepository());
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
