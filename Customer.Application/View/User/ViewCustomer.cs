using Customer.Application.Services;
using Customer.Application.Services.User;
using Customer.Application.View.Warehouse;
using Customer.Domain.Interfaces;
using Customer.Domain.Model.User;
using Customer.Domain.Model.User.LoginModel;
using Customer.Domain.Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.View.User
{
    public class ViewCustomer
    {
        private readonly ViewWarehouse warehouse = new ViewWarehouse();
        private readonly ViewShoppingCart shoppingCart;
        private Title title = new Title();

        public ViewCustomer(IShoppingCart shoppingCart)
        {
            this.shoppingCart = new ViewShoppingCart(shoppingCart);
        }

        private bool isRunning = true;
        public bool ShowMenu(string username)
        {
            while (isRunning)
            {
                Console.WriteLine("1.Dostępne Produkty");
                Console.WriteLine("2.Wyszukaj Produkt");
                Console.WriteLine("3.Koszyk");
                Console.WriteLine("4.Ustawienia");
                Console.WriteLine("0.Wyjście");
                var operation = Console.ReadLine();
                isRunning = ViewMenuCustomer(operation,username);
                
            }

            return isRunning;
        }
        private bool ViewMenuCustomer(string operation, string username)
        {
            
            switch (operation)
            {
                case "1":
                    Console.Clear();  
                    isRunning = warehouse.DisplayAllProducts();
                    break;

                case "2":
                    Console.Clear();
                    isRunning = warehouse.SearchProduct();
                    break;
                case"3":
                    Console.Clear();
                    isRunning = shoppingCart.ShoppingCart();
                    break;
                case "4":
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
            var manager = new DataChangeCustomer();

            var clientLogin = manager.GetClientLogin(username);

            if (clientLogin != null)
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
                        manager.UpdateClientUsername(clientLogin.Id, newUsername);
                        break;
                    case "2":
                        Console.Write("Nowe Hasło");
                        manager.UpdateClientPassword(clientLogin.Id);
                        break;
                    case "3":
                        Console.Write("Nowe imię: ");
                        var newFirstName = Console.ReadLine();
                        manager.UpdateClientFirstName(clientLogin.Id, newFirstName);
                        break;
                    case "4":
                        Console.Write("Nowe nazwisko: ");
                        var newLastName = Console.ReadLine();
                        manager.UpdateClientLastName(clientLogin.Id, newLastName);
                        break;
                    case "5":
                        Console.Write("Nowy email: ");
                        var newEmail = Console.ReadLine();
                        manager.UpdateClientEmail(clientLogin.Id, newEmail);
                        break;
                    case "6":
                        Console.Write("Nowy numer telefonu: ");
                        var newPhoneNumber = Console.ReadLine();
                        manager.UpdateClientPhoneNumber(clientLogin.Id, newPhoneNumber);
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
