using Customer.Application.Services.User;
using Customer.Application.View.UserManagement.Registration;
using Customer.Application.View.Warehouse;
using Customer.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.View.User.Administrator
{
    public class ViewAdministrator
    {
        private Title title = new Title();
        private bool isRunning = true;
        public bool ShowMenu()
        {
            while (isRunning)
            {
                    Console.WriteLine("Zarządzanie Aplikacją" + "\n");
                    Console.WriteLine("1.Zarządzanie Klientami");
                    Console.WriteLine("2.Zarządzanie Dostawcami");
                    Console.WriteLine("3.Zarządzanie Magazynem");
                    Console.WriteLine("4.Zarządzanie Pracownikami");
                    Console.WriteLine("5.Raport");
                    Console.WriteLine("0. Wyjście");
                    var operation = Console.ReadLine();
                    isRunning = ViewAdminMenu(operation);
            }

            return isRunning;
        }
        private bool ViewAdminMenu(string operation)
        {
            switch (operation)
            {
                case "1":
                    Console.Clear();
                    title.ViewTitle();
                    DisplayCustomerManagementOptions();
                    break;

                case "2":
                    Console.Clear();
                    title.ViewTitle();
                    DisplaySupplierManagementOptions();
                    break;
                case "3":
                    Console.Clear();
                    ViewWarehouse viewWarehouse = new ViewWarehouse();
                    viewWarehouse.Warehouse();
                    break;
                case "4":
                    Console.Clear();
                    title.ViewTitle();
                    DisplayEmployeeManagementOptions();
                    break;
                case "5":
                    Console.Clear();
                    RaportGenerator raport = new RaportGenerator();
                    raport.GenerateReport();
                    break;
                case "0":
                    Console.Clear();
                    return false;
                default:
                    Console.WriteLine("Nieprawidłowa operacja! Spróbuj ponownie!!");
                    break;
            }

            return true;
        }
        private bool DisplayCustomerManagementOptions()
        {
            while (isRunning)
            {
                Console.WriteLine("Panel zarządzania klientami!");
                Console.WriteLine("1. Wyświetl listę klientów");
                Console.WriteLine("2. Usuń klienta");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                string choice = Console.ReadLine();
                isRunning = ManageCustomers(choice);
            }

            return true;
        }
        public bool ManageCustomers(string choice)
        {
            switch (choice)
            {
                case "1":
                    EmployeeService.DisplayCustomerList();
                    break;
                case "2":
                    Console.Write("Podaj ID klienta do usunięcia: ");
                    string customerId = Console.ReadLine();
                    EmployeeService.RemoveCustomer(customerId);
                    break;
                case "0":
                    Console.Clear();
                    title.ViewTitle();
                    return false;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
            return true;

        }
        private bool DisplaySupplierManagementOptions()
        {
            while (isRunning)
            {
                Console.WriteLine("Panel zarządzania dostawcami!");
                Console.WriteLine("1. Wyświetl listę dostawców");
                Console.WriteLine("2. Usuń klienta");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                string choice = Console.ReadLine();
                isRunning = ManageSupplier(choice);
            }
            return true;
        }
        private bool ManageSupplier(string choice)
        {
            switch (choice)
            {
                case "1":
                    EmployeeService.DisplaySupplierList();
                    break;
                case "2":
                    Console.Write("Podaj ID dostawcy do usunięcia: ");
                    string supplierId = Console.ReadLine();
                    EmployeeService.RemoveSupplier(supplierId);
                    break;
                case "0":
                    Console.Clear();
                    title.ViewTitle();
                    return false;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
            return true;
        }
        private bool DisplayEmployeeManagementOptions()
        {
            while (isRunning)
            {
                Console.WriteLine("Panel zarządzania pracownikami!");
                Console.WriteLine("1. Rejestracja Pracownika");
                Console.WriteLine("2. Usuń Pracownika");
                Console.WriteLine("3. Lista pracowników");
                Console.WriteLine("0. Wyjście");
                Console.WriteLine("Wybierz opcje: ");
                string choice = Console.ReadLine();
                isRunning = ManageEmployee(choice);
            }
            return true;
        }

        private bool ManageEmployee(string choice)
        {
            switch (choice)
            {   
                case "1":
                    RegisterEmployee employee = new RegisterEmployee();
                    employee.RegistrationEmployee();
                    break;
                case "2":
                    Console.Write("Podaj ID Pracownika do usunięcia: ");
                    string employeeId = Console.ReadLine();
                    EmployeeService.RemoveSupplier(employeeId);
                    break;
                case "3":
                    EmployeeService.DisplayEmployeeList();
                    break;
                case "0":
                    Console.Clear();
                    title.ViewTitle();
                    return false;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
            return true;
        }
    }
}

