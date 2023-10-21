using Customer.Domain.Model.User.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.View.UserManagement.Registration
{
    public class ViewRegistration
    {
        private readonly Title title = new Title();
        public void ViewRegister() 
        {
                title.ViewTitle();
                Console.WriteLine("Wybierz rodzaj rejestracji: ");
                Console.WriteLine("1. Rejestracja klienta");
                Console.WriteLine("2. Rejestracja dostawcy");
                Console.WriteLine("0. Wyjście" + "\n");
                Console.Write("Wybierz numer opcji: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        title.ViewTitle();
                        RegisterCustomer viewRegisterCustomer = new RegisterCustomer();
                        viewRegisterCustomer.RegistrationCustomer();
                        break;
                    case "2":
                        Console.Clear();
                        title.ViewTitle();
                        RegisterSupplier viewRegisterSupplier = new RegisterSupplier();
                        viewRegisterSupplier.RegistrationSupplier();
                        break;
                    case "0":
                    Console.Clear();
                    return;
                    default: Console.WriteLine("Nie prawidłowa operacja!"); return;

                }

        }

    }
}
