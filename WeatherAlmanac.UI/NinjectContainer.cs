using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.BLL;
using WeatherAlmanac.Core.Interfaces;
using Ninject;
using WeatherAlmanac.DAL;

namespace WeatherAlmanac.UI
{
    public static class NinjectContainer
    {
        public static StandardKernel Kernel { get; set; }
        public static void Configure(ApplicationMode mode)
        {
            Kernel = new StandardKernel();

            if(mode == ApplicationMode.TEST)
            {
                Kernel.Bind<IRecordRepository>().To<MockRecordRepository>();
            }

            Kernel.Bind<IRecordService>().To<RecordService>();
        }
    }
}
