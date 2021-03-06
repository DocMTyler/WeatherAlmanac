using System;
using WeatherAlmanac.Core.Interfaces;
using WeatherAlmanac.Core.DTO;
using System.IO;
using System.Linq;

namespace WeatherAlmanac.UI
{
    class MenuController
    {
        private ConsoleIO _ui;
        public IRecordService Service { get; set; }

        public MenuController(ConsoleIO ui)
        {
            _ui = ui;
        }

        public ApplicationMode Setup()
        {
            _ui.Display("Enter Application Mode: ");
            _ui.Display("1. Test");
            _ui.Display("2. Live");
            _ui.Display(Directory.GetCurrentDirectory() + @"\Data\DateRecord.csv");

            int mode = _ui.GetInt("");
            if(mode == 1)
            {
                return ApplicationMode.TEST;
            }else if(mode == 2)
            {
                return ApplicationMode.LIVE;
            }
            else
            {
                _ui.Error("Invalid mode. Exiting.");
                Environment.Exit(0);
                return ApplicationMode.TEST;
            }
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                switch (GetMenuChoice())
                {
                    case 1:
                        LoadRecord();
                        break;
                    case 2:
                        ViewRecordsByDateRange();
                        break;
                    case 3:
                        AddRecord();
                        break;
                    case 4:
                        EditRecord();
                        break;
                    case 5:
                        DeleteRecord();
                        break;
                    case 6:
                        AutoAddRecords();
                        break;
                    case 7:
                        StatsByRange();
                        break;
                    case 8:
                        running = false;
                        break;
                    default:
                        _ui.Display("Invalid input. Please enter a number 1 - 7");
                        break;
                }
            }
        }

        public int GetMenuChoice()
        {
            DisplayMenu();
            bool isValid = int.TryParse(Console.ReadLine(), out int output);
            while(!isValid)
            {
                _ui.Display("Invalid entry, enter a number 1 - 7");
                isValid = int.TryParse(Console.ReadLine(), out output);
            }
            return output;
        }

        public void DisplayMenu()
        {
            _ui.Display("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
            _ui.Display("Main Menu");
            _ui.Display("=========");
            _ui.Display("1. Load a record");
            _ui.Display("2. View records by Date Range");
            _ui.Display("3. Add Record");
            _ui.Display("4. Edit Record");
            _ui.Display("5. Delete Record");
            _ui.Display("6. Auto Add Records");
            _ui.Display("7. Stats by Range");
            _ui.Display("8. Quit");
            _ui.Display("Please enter a selection 1 - 8");
        }

        public void LoadRecord()
        {
            DateTime loadDate;
            _ui.Display("Enter a date mm/dd/yyy");
            loadDate = ValiDATE(Console.ReadLine());
            var record = Service.Get(loadDate);
           
            _ui.Display(record.Message);
            if (!record.Success) return;
            Console.WriteLine(record.Data.ToString());
        }

        public void ViewRecordsByDateRange()
        {
            _ui.Display("Enter a start date mm/dd/yyy");
            DateTime start = ValiDATE(Console.ReadLine());
            _ui.Display("Enter a end date mm/dd/yyy");
            DateTime end = ValiDATE(Console.ReadLine());

            var service = Service.LoadRange(start, end);
            
            foreach(var record in service.Data)
            {
                _ui.Display(record.ToString());
            }
            
            _ui.Display(service.Message);
        }

        public void StatsByRange()
        {
            
            _ui.Display("Enter a start date mm/dd/yyy");
            DateTime start = ValiDATE(Console.ReadLine());
            _ui.Display("Enter a end date mm/dd/yyy");
            DateTime end = ValiDATE(Console.ReadLine());

            Service.StatsRange(start, end);
        }
        
        public void AutoAddRecords()
        {
            string path = Directory.GetCurrentDirectory() + @"\Data\DateRecord.csv";
            var service = Service.AutoAddRecords(path);
            _ui.Display(service.Message);
        }

        public void AddRecord()
        {
            DateRecord recordToAdd = new DateRecord();
            _ui.Display("Enter a date mm/dd/yyy");
            recordToAdd.Date = ValiDATE(Console.ReadLine());
            _ui.Display("Enter a HighTemp: ");
            recordToAdd.HighTemp = ValiDecimal(Console.ReadLine());
            _ui.Display("Enter a LowTemp: ");
            recordToAdd.LowTemp = ValiDecimal(Console.ReadLine());
            _ui.Display("Enter a Humidity: ");
            recordToAdd.Humidity = ValiDecimal(Console.ReadLine());
            _ui.Display("Enter a Description: ");
            recordToAdd.Description = Console.ReadLine();

            var service = Service.Add(recordToAdd);
            _ui.Display(service.Message);
        }

        public void EditRecord()
        {
            _ui.Display("Enter a date mm/dd/yyyy");
            DateTime loadDate = ValiDATE(Console.ReadLine());
            
            var loadedRecord = Service.Get(loadDate);
            if(!loadedRecord.Success)
            {
                _ui.Display(loadedRecord.Message);
                return;
            }
            _ui.Display(loadedRecord.Data.ToString());

            _ui.Display("Enter the new data");
            DateRecord recordToEdit = new DateRecord();
            recordToEdit.Date = loadDate;
            _ui.Display("Enter a HighTemp: ");
            recordToEdit.HighTemp = ValiDecimal(Console.ReadLine());
            _ui.Display("Enter a LowTemp: ");
            recordToEdit.LowTemp = ValiDecimal(Console.ReadLine());
            _ui.Display("Enter a Humidity: ");
            recordToEdit.Humidity = ValiDecimal(Console.ReadLine());
            _ui.Display("Enter a Description: ");
            recordToEdit.Description = Console.ReadLine();

            var service = Service.Edit(recordToEdit);
            _ui.Display(service.Message);

            _ui.Display("\nEdited Data\n===========");
            _ui.Display(service.Data.ToString());
        }

        public void DeleteRecord()
        {
            DateTime removeDate;
            _ui.Display("Enter a date to remove mm/dd/yyyy");
            removeDate = ValiDATE(Console.ReadLine());
            var loadedRemoveDate = Service.Get(removeDate);

            var service = Service.Remove(removeDate);
            _ui.Display(service.Message);
        }

        public DateTime ValiDATE(string input)
        {
            bool isValid = DateTime.TryParse(input, out DateTime date);
            while (!isValid)
            {
                _ui.Display("Not a valid date format, please try again");
                isValid = DateTime.TryParse(Console.ReadLine(), out date);
            }
            return date;
        }

        public Decimal ValiDecimal(string input)
        {
            bool isValid = Decimal.TryParse(input, out Decimal output);
            while (!isValid)
            {
                _ui.Display("Not a valid date format, please try again");
                isValid = Decimal.TryParse(Console.ReadLine(), out output);
            }
            return output;
        }
    }
}
