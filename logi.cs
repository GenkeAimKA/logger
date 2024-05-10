using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;

namespace logger
{
    internal class logi
    {
        /**Перечисление уровней логирования*/
        public enum LogLevel 
        {
            TRACE,
            DEBUG,
            INFO,         
            WARNING,
            ERROR
        }
        /**Перечисление режимов вывода логов:
        *CONSOLE - вывод только в консоль
        *FILE - вывод только файлом
        *CONSOLE_FILE - вывод в консоль и файлом
        */
        public enum Output
        {
            CONSOLE,
            FILE,
            CONSOLE_FILE
        }

        /**Хранение имени файла*/
        private static string _fileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        /**Расширение файла*/
        private static readonly string FILE_EXTENSION = ".log";
        /**Хранение текущего уровня логирования. Сейчас TRACE*/
        private static LogLevel _logLevel= LogLevel.TRACE; 
        /**Храние текущего уровня вывода записей. Сейчас CONSOLE*/
        private static Output _output = Output.CONSOLE;
        /**Введение журнала. false - все в одном, true -все отдельно */
        private static bool _loggining = false; 
        /** Необходимость гененрации новых файлов при каждом новом запуске программы*/
        private static bool _newFile = false;
        /**Текущая дата при необходимости генерации новых файло при каждом запуске программы*/
        private static readonly string _dateTime = DateTime.Now.ToString("G");
        /**Свойства
         *Установление или получение текущего уровня логов*/
        public static LogLevel logLevel 
        {
            get { return _logLevel; }
            set { _logLevel = value; }
        }
        /**Устонавление или получение текущего режима записи логов*/
        public static Output output 
        {
            get { return _output; }
            set { _output = value; }
        }
        /**Установление или получение имени файла логов*/
        public static string FileName 
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        /**Установка или получение раздельного ведения журнала*/
        public static bool Loggining 
        {
            get { return _loggining; }
            set { _loggining = value; }
        }
        /**Установка или получение необходимости генерации новых имен для каждого нового запуска программы*/
        public static bool NewFile 
        {
            get { return _newFile; }
            set { _newFile = value; }
        }

        /**Методы
        *Процедура записи логов
        *logLevelM - уровень лога
        *message - ссылка на сообщение для вывода в лог
        *filePath - ссылка на путь до файла программы, откуда совершен вызов
        *member - ссылка на название функции, откуда совершен вызов
        *line - ссылка на номер строки программы, откуда совершен вызов
        */
        private static void WriteLog(LogLevel logLevelM, ref string message, ref string filePath, ref string member, ref int line)
        {
            if (logLevelM < _logLevel) //вывод логов по минимальному уровню
                return;
            //формат тескта лога
            string text = $"[{DateTime.Now}] | {logLevelM} | {filePath}\\{member}:{line} -> {message}\n";
            //Формирование имени файла 
            string fileName = FileName;
            if (Loggining) //в зависимости от режима ведения журнала
                fileName += "_" + logLevelM.ToString();
            if (NewFile) //в зависимости от необходимости генерации новых имен файла для каждого нового запуска
                fileName += "_" + _dateTime;
            fileName += FILE_EXTENSION;

            //Запись лога согласно настроенному режиму записи (WriteMode)
            switch (_output)
            {
                case Output.CONSOLE:
                    Console.Write(text); //Вывод на консоль
                    break;
                case Output.FILE:
                    File.AppendAllText(fileName, text); //Запись лога в файл
                    break;
                case Output.CONSOLE_FILE:
                    Console.Write(text);
                    File.AppendAllText(fileName, text); //Запись лога в файл
                    break;
                default:
                    Console.Write("Неопределенный режим записи. " + text); //Вывод на консоль
                    break;
            }

        }

        /**Процедура вызова записи логов
         *message - сообщение для вывода в лог
         *filePath - путь до файла программы, откуда был совершен вызов
         *member - функция, откуда был совершен вызов
         *line - строка программы, откуда был совершен вызов*/
        public static void Trace(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            WriteLog(LogLevel.TRACE, ref message, ref filePath, ref member, ref line);
        }
        public static void Debug(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            WriteLog(LogLevel.DEBUG, ref message, ref filePath, ref member, ref line);
        }
        public static void Info(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            WriteLog(LogLevel.INFO, ref message, ref filePath, ref member, ref line);
        }
        public static void Warning(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            WriteLog(LogLevel.WARNING, ref message, ref filePath, ref member, ref line);
        }
        public static void Error(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            WriteLog(LogLevel.ERROR, ref message, ref filePath, ref member, ref line);
        }

       
    }
}
