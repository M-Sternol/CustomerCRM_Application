using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Customer.DataStorage
{
    public class LogToFileMessage
    {
        public static void LogError(string message, string stackTrace)
        {
            LogToJSON("ERROR", message, stackTrace, FileLocations.GetLogErrorFilePath());
        }

        public static void LogSuccess(string message)
        {
            LogToJSON("SUCCESS", message, null, FileLocations.GetLogSuccessFilePath());
        }

        private static void LogToJSON(string type, string message, string stackTrace, string logFilePath)
        {
            try
            {
                var logObject = new
                {
                    Timestamp = DateTime.Now,
                    Type = type,
                    Message = message,
                    StackTrace = stackTrace
                };

                string logMessage = JsonConvert.SerializeObject(logObject);

                if (!File.Exists(logFilePath))
                {
                    using (StreamWriter sw = File.CreateText(logFilePath))
                    {
                        sw.WriteLine(logMessage);
                    }
                }
                else
                {
                    File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu do pliku dziennika: {ex.Message} w pliku {ex.StackTrace}");
            }
        }
    }
}
