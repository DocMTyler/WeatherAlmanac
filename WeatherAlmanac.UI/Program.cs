using System;
using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.BLL;
using WeatherAlmanac.Core.Interfaces;
using Ninject;

namespace WeatherAlmanac.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var controller = NinjectContainer.Kernel.Get<MenuController>();
            ConsoleIO ui = new ConsoleIO();
            MenuController menu = new MenuController(ui);
            ApplicationMode mode = menu.Setup();
            IRecordService service = RecordServiceFactory.GetRecordService(mode);
            menu.Service = service;
            menu.Run();
        }
    }
}
