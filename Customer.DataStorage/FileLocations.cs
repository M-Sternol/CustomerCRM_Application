using Customer.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.DataStorage
{
    public static class FileLocations
    {
        private static string BaseDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileDirectory");
        private static string LogDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LoggMessage");
        private static string ReportArchive => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportArchive");
        public static string GetFilePath(string fileName)
        {
            return Path.Combine(BaseDirectory, fileName);
        }
        public static string GetFilePathLogApp(string fileName)
        {
            return Path.Combine(LogDirectory, fileName);
        }
        public static string GetFilePathReportArchive()
        {
            return Path.Combine(ReportArchive);
        }

        public static string GetEmployeeFilePath()
        {
            return GetFilePath("Employee.json");
        }

        public static string GetCustomerFilePath()
        {
            return GetFilePath("Customers.json");
        }

        public static string GetSupplierFilePath()
        {
            return GetFilePath("Supplier.json");
        }
        public static string GetUserLoginsFilePath()
        {
            return GetFilePath("UserLogins.json");
        }

        public static string GetWarehouseFilePath()
        {
            return GetFilePath("Products.json");
        }

        public static string GetLogErrorFilePath()
        {
            return GetFilePathLogApp("LogError.json");
        }
        public static string GetLogSuccessFilePath()
        {
            return GetFilePathLogApp("LogSuccess.json");
        }
        
    }
}

