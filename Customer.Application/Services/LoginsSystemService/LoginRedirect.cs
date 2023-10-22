using Customer.Application.Interfaces;
using Customer.Application.View;
using Customer.Application.View.User;
using Customer.Application.View.UserManagement.Login;
using Customer.Domain.Interfaces;
using Customer.Application.View;
using Customer.Domain.Model.User.LoginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.Application.View.User.Administrator;

namespace Customer.Application.Services.LoginsSystemService
{
    public class LoginRedirect : ILoginRedirect
    {
        private IShoppingCart shoppingCart;
        private readonly Title title = new Title();
        public void RedirectUser(string accountType, string username)
        {
            title.ViewTitle();
            if (accountType == "Klient")
            {
                bool isRunning = true;
                while (isRunning)
                {
                    Console.WriteLine("Cześć!" + " " + username + "\n");
                    ViewCustomer customer = new ViewCustomer(shoppingCart);
                    isRunning = customer.ShowMenu(username);
                }
            }
            else if (accountType == "Dostawca")
            {
                bool isRunning = true;
                while (isRunning)
                {
                    Console.WriteLine("Cześć!" + " " + username + "\n");
                    ViewSupplier supplier = new ViewSupplier();
                    isRunning = supplier.ShowMenu(username);
                }
            }
            else if (accountType == "Administrator" || accountType == "Manager" || accountType == "Specjalista" || accountType == "Kierownik" || accountType == "Asystent" || accountType == "Pracownik")
            {
                bool isRunning = true;
                while (isRunning)
                {
                    Console.WriteLine($"Typ Konta: {accountType}");
                    Console.WriteLine("Cześć!" + " " + username + "\n");
                    ViewAdministrator administrator = new ViewAdministrator();
                    isRunning = administrator.ShowMenu();
                }
            }
            else
            {
                Console.WriteLine("Nieznany typ konta. Spróbuj ponownie.");
            }
        }
    }
}
