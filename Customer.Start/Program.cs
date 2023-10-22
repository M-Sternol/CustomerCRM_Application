using Customer.Application.Helpers.Utils;
using Customer.Application.Helpers;
using Customer.Application.View.UserManagement.Login;
using Customer.Application.View.UserManagement.Registration;
using Customer.Application.View.Warehouse;
using Customer.DataStorage;
using Customer.Application.View;

namespace Customer.Start
{
    public class Program
    {
        static void Main(string[] args)
        {
            Title title = new Title();
            bool isRunning = true;
            while (isRunning)
            {
                title.ViewTitle();
                Console.WriteLine("1. Zaloguj się");
                Console.WriteLine("2. Zarejestruj się");
                Console.WriteLine("3. Dostępne walidacje");
                Console.WriteLine("0. Wyjdź" + "\n");
                string operation = Console.ReadLine();

                switch (operation)
                {
                    case "1":
                        Console.Clear();
                        LoginSystem loginSystem = new LoginSystem();
                        loginSystem.LoginsSystem();
                        break;
                    case "2":
                        Console.Clear();
                        ViewRegistration registration = new ViewRegistration();
                        registration.ViewRegister();
                        break;
                    case "3":
                        UserDocumentation documentation = new UserDocumentation();
                        documentation.DisplayDocumentation();
                        break;
                        case "4":
                           RegisterEmployee registerEmployee = new RegisterEmployee();
                        registerEmployee.RegistrationEmployee(); //Awaryjne Rejestrowanie pracownika
                        break;
                    case "0":
                        Console.WriteLine("Do Zobaczenia!");
                        isRunning = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidłowa operacja! Spróbuj ponownie");
                        break;
                }
            }
        }
    }
}