using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.DataStorage
{
    public class RaportGenerator
    {
        private static string ReportDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
        private static string ReportFileName = "ReportArchive";

        public void GenerateReport()
        {
            Console.WriteLine("Wybierz rodzaj raportu:" + "\n");
            Console.WriteLine("1. Indywidualne raporty");
            Console.WriteLine("2. Raport całkowity");
            Console.WriteLine("3. Raport błędów");
            Console.WriteLine("4. Raport sukcesów");
            Console.WriteLine("5. Raport Ilości użytkowników");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        GenerateIndividualReports();
                        break;
                    case 2:
                        GenerateTotalReport();
                        break;
                    case 3:
                        GenerateErrorReport();
                        break;
                    case 4:
                        GenerateSuccessReport();
                        break;
                    case 5:
                        GenerateUserReport();
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
        }

        public static void GenerateIndividualReports()
        {
            Console.WriteLine("Wybierz rodzaj indywidualnego raportu:");
            Console.WriteLine("1. Administratorzy");
            Console.WriteLine("2. Klienci");
            Console.WriteLine("3. Dostawcy");
            Console.WriteLine("4. Magazyny");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                string reportName = "";
                string filePath = "";

                switch (choice)
                {
                    case 1:
                        reportName = "Administratorzy";
                        filePath = FileLocations.GetEmployeeFilePath();
                        break;
                    case 2:
                        reportName = "Klienci";
                        filePath = FileLocations.GetCustomerFilePath();
                        break;
                    case 3:
                        reportName = "Dostawcy";
                        filePath = FileLocations.GetSupplierFilePath();
                        break;
                    case 4:
                        reportName = "Magazyny";
                        filePath = FileLocations.GetWarehouseFilePath();
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór.");
                        return;
                }
                Console.Clear();
                Console.WriteLine($"Generowanie raportu: {reportName}...");
                GenerateAndSaveReport(reportName, filePath);
                Console.WriteLine($"Raport {reportName} został wygenerowany i zapisany.");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
        }


        public static void GenerateTotalReport()
        {
            Console.WriteLine("Generowanie raportu całkowitego...");

            var adminData = ReadFile(FileLocations.GetEmployeeFilePath());
            var customerData = ReadFile(FileLocations.GetCustomerFilePath());
            var supplierData = ReadFile(FileLocations.GetSupplierFilePath());
            var warehouseData = ReadFile(FileLocations.GetWarehouseFilePath());

            var reportData = new
            {
                Administratorzy = adminData,
                Klienci = customerData,
                Dostawcy = supplierData,
                Magazyny = warehouseData,
            };

            SaveReportToFile(reportData, ReportFileName, "RaportCalkowity.json");

            Console.WriteLine("Raport całkowity został wygenerowany i zapisany.");
        }

        public static void GenerateErrorReport()
        {
            Console.WriteLine("Generowanie raportu błędów...");

            var errorData = ReadFile(FileLocations.GetLogErrorFilePath());

            var reportData = new
            {
                Bledy = errorData
            };

            SaveReportToFile(reportData, ReportFileName, "RaportBledow.json");

            Console.WriteLine("Raport błędów został wygenerowany i zapisany.");
        }

        public static void GenerateSuccessReport()
        {
            Console.WriteLine("Generowanie raportu sukcesów...");

            var successData = ReadFile(FileLocations.GetLogSuccessFilePath());

            var reportData = new
            {
                Sukcesy = successData
            };

            SaveReportToFile(reportData, ReportFileName, "RaportSukcesow.json");

            Console.WriteLine("Raport sukcesów został wygenerowany i zapisany.");
        }
        public static void GenerateUserReport()
        {
            Console.WriteLine("Generowanie raportu użytkowników...");

            var userData = ReadFile(FileLocations.GetUserLoginsFilePath());
            var userCount = userData.Count;
            var reportData = new
            {
                User = userData,
                TotalUser = userCount
            };

            SaveReportToFile(reportData, ReportFileName, "RaportUzytkownikow.json");

            Console.WriteLine($"Raport użytkowników został wygenerowany i zapisany. Liczba użytkowników: {userCount}.");
        }
        private static void GenerateAndSaveReport(string dataName, string filePath)
        {
            var data = ReadFile(filePath);
            var reportData = new { Data = dataName, Values = data };
            SaveReportToFile(reportData, ReportFileName, $"{dataName}_raport.json");
        }

        private static List<string> ReadFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return new List<string>(File.ReadAllLines(filePath));
                }
                else
                {
                    Console.WriteLine($"Plik nie istnieje: {filePath}");
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu pliku: {filePath}. Szczegóły: {ex.Message}");
                return new List<string>();
            }
        }

        private static void SaveReportToFile(object data, string folderName, string fileName)
        {
            try
            {
                var reportDirectory = Path.Combine(ReportDirectory, folderName);
                if (!Directory.Exists(reportDirectory))
                {
                    Directory.CreateDirectory(reportDirectory);
                }

                var fullPath = Path.Combine(reportDirectory, fileName);
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(fullPath, json);
                Console.WriteLine($"Raport został zapisany do pliku: {fullPath}");
                LogToFileMessage.LogSuccess(fileName + reportDirectory);

                Process.Start(new ProcessStartInfo
                {
                    FileName = fullPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Generate Report: {ex.Message}");
                LogToFileMessage.LogError($"Error Generate Report: {ex.Message}", ex.StackTrace);
            }
        }
    }
}
