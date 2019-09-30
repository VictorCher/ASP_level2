using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public static class Log4NetExtentions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory Factory, string ConfigurationFile = "log4net.config")
        {
            if (!Path.IsPathRooted(ConfigurationFile)) // Относительный путь к файлу конфигурации относительно рабочего каталога
            {
                var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("На найдена сборка исполнительного фалйа");
                var dir = Path.GetDirectoryName(assembly.Location) ?? throw new InvalidOperationException("Не определена дирректория исполнительного файла");
                ConfigurationFile = Path.Combine(dir, ConfigurationFile);
            }

            Factory.AddProvider(new Log4NetLoggerProvider(ConfigurationFile));
            return Factory;
        }
    }
}