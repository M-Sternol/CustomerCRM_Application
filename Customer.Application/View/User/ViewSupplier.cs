using Customer.Application.Services.User;
using Customer.Application.View.Warehouse;
using Customer.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.View.User
{
    public class ViewSupplier
    {
        private bool isRunning = true;
        private Title title = new Title();
        public bool ShowMenu(string username)
        {
            while (isRunning)
            {
                Console.WriteLine("1.Zarządzanie Produktami");
                Console.WriteLine("2.Ustawienia");
                Console.WriteLine("0.Wyjście");
                var operation = Console.ReadLine();
                isRunning = ViewMenuSupplier(operation, username);
            }

            return isRunning;
        }
        private bool ViewMenuSupplier(string operation,string username)
        {
            switch (operation)
            {
                case "1":
                    Console.Clear();
                    ViewWarehouse warehouse = new ViewWarehouse();
                    isRunning = warehouse.Warehouse();
                    break;

                case "2":
                    Console.Clear();
                    title.ViewTitle();
                    ViewDataChange(username);
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
        private void ViewDataChange(string username)
        {
            var manager = new DataChangeSupplier();

            var userLogin = manager.GetSupplierLogin(username);

            if (userLogin != null)
            {

                Console.WriteLine("Co chcesz zmienić?");
                Console.WriteLine("1. Nazwa użytkownika");
                Console.WriteLine("2. Hasło");
                Console.WriteLine("3. Imię");
                Console.WriteLine("4. Nazwisko");
                Console.WriteLine("5. Email");
                Console.WriteLine("6. Numer telefonu");
                Console.WriteLine("0. Wyjście");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Nowa nazwa użytkownika: ");
                        var newUsername = Console.ReadLine();
                        manager.UpdateSupplierUsername(userLogin.Id, newUsername);
                        break;
                    case "2":
                        Console.Write("Nowe Hasło");
                        manager.UpdateSupplierPassword(userLogin.Id);
                        break;
                    case "3":
                        Console.Write("Nowe imię: ");
                        var newFirstName = Console.ReadLine();
                        manager.UpdateSupplierFirstName(userLogin.Id, newFirstName);
                        break;
                    case "4":
                        Console.Write("Nowe nazwisko: ");
                        var newLastName = Console.ReadLine();
                        manager.UpdateSupplierLastName(userLogin.Id, newLastName);
                        break;
                    case "5":
                        Console.Write("Nowy email: ");
                        var newEmail = Console.ReadLine();
                        manager.UpdateSupplierEmail(userLogin.Id, newEmail);
                        break;
                    case "6":
                        Console.Write("Nowy numer telefonu: ");
                        var newPhoneNumber = Console.ReadLine();
                        manager.UpdateSupplierPhoneNumber(userLogin.Id, newPhoneNumber);
                        break;
                    case "0":
                        Console.Clear();
                        title.ViewTitle();
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta na podstawie podanej nazwy użytkownika.");
            }
        }
    }
}
