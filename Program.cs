using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logger
{           /**Журнал логов: logger\bin\Debug */
    internal class Program
    {
        static void Main(string[] args)
        {
            //Настройка лога
            logi.logLevel = logi.LogLevel.DEBUG; //Минимальный уровень отображения логов
            logi.output = logi.Output.CONSOLE; // Режим записи логов
            logi.Loggining = true; // Раздельное ведение журнала логов
            logi.NewFile = true; // Новые имена при каждом новом запуске
            //Вывод логов
            logi.Debug("DEBUG");//Вывод лога DEBUG
            logi.Info("INFO");//Вывод лога INFO
            logi.Warning("WARNING");//Вывод лога WARNING
            logi.Error("ERROR");//Вывод лога ERROR
            Console.ReadKey();
        }
    }
}
