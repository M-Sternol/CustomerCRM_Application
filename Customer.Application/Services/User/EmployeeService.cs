using Customer.DataStorage;
using Customer.Domain.Model.User.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Customer.Application.Services.User
{
    public class EmployeeService
    {
        private static List<ClientModel> clients;
        private static List<SupplierModel> suppliers;
        private static List<EmployeeModel> employees; // Dodane pole dla pracowników

        public EmployeeService()
        {
            LoadData();
        }

        private static void LoadData()
        {
            string customerFilePath = FileLocations.GetCustomerFilePath();
            if (File.Exists(customerFilePath))
            {
                string customerJsonData = File.ReadAllText(customerFilePath);
                clients = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClientModel>>(customerJsonData);
            }
            else
            {
                clients = new List<ClientModel>();
            }

            string supplierFilePath = FileLocations.GetSupplierFilePath();
            if (File.Exists(supplierFilePath))
            {
                string supplierJsonData = File.ReadAllText(supplierFilePath);
                suppliers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SupplierModel>>(supplierJsonData);
            }
            else
            {
                suppliers = new List<SupplierModel>();
            }

            string employeeFilePath = FileLocations.GetEmployeeFilePath(); // Pobranie ścieżki pliku dla pracowników
            if (File.Exists(employeeFilePath))
            {
                string employeeJsonData = File.ReadAllText(employeeFilePath);
                employees = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmployeeModel>>(employeeJsonData);
            }
            else
            {
                employees = new List<EmployeeModel>();
            }
        }

        private static void SaveData()
        {
            string customerFilePath = FileLocations.GetCustomerFilePath();
            string customerJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(clients, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(customerFilePath, customerJsonData);

            string supplierFilePath = FileLocations.GetSupplierFilePath();
            string supplierJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(suppliers, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(supplierFilePath, supplierJsonData);

            string employeeFilePath = FileLocations.GetEmployeeFilePath(); // Pobranie ścieżki pliku dla pracowników
            string employeeJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(employeeFilePath, employeeJsonData);
        }

        public static void RemoveCustomer(string customerId)
        {
            ClientModel customerToRemove = clients.FirstOrDefault(c => c.Id == customerId);
            if (customerToRemove != null)
            {
                clients.Remove(customerToRemove);
                Console.WriteLine("Klient został usunięty!");
                SaveData();
            }
        }

        public static void RemoveSupplier(string supplierId)
        {
            SupplierModel supplierToRemove = suppliers.FirstOrDefault(s => s.Id == supplierId);
            if (supplierToRemove != null)
            {
                suppliers.Remove(supplierToRemove);
                Console.WriteLine("Dostawca został usunięty!");
                SaveData();
            }
        }

        public static void RemoveEmployee(string employeeId)
        {
            EmployeeModel employeeToRemove = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
                Console.WriteLine("Pracownik został usunięty!");
                SaveData();
            }
        }

        public static void DisplayCustomerList()
        {
            LoadData();
            Console.WriteLine("Lista klientów: " + "\n");
            foreach (var customer in clients)
            {
                Console.WriteLine($"ID: {customer.Id}, Nazwa użytkownika: {customer.Username}, Imię: {customer.FirstName}, Nazwisko: {customer.LastName}");
            }
        }

        public static void DisplaySupplierList()
        {
            LoadData();
            Console.WriteLine("Lista dostawców: " + "\n");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"ID: {supplier.Id}, Nazwa: {supplier.CompanyName}, {supplier.FirstName}, {supplier.LastName}, Email: {supplier.Email}, Numer telefonu: {supplier.PhoneNumber}");
            }
        }

        public static void DisplayEmployeeList()
        {
            LoadData();
            Console.WriteLine("Lista pracowników: " + "\n");
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.EmployeeId},Typ: {employee.AccountType},Stanowisko: {employee.Position}, Imię: {employee.FirstName}, Nazwisko: {employee.LastName}, Email: {employee.Email}, Numer telefonu: {employee.PhoneNumber}");
            }
        }
    }
}
