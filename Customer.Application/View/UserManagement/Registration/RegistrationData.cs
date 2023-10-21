using Customer.Domain.Model.User.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.View.UserManagement.Registration
{
    public class RegistrationData
    {
        public void CustomerRegistrationData(ClientModel _clientModel)
        {
            Console.Clear();
            Console.WriteLine("Zarejestrowano nowego klienta: " + _clientModel.FirstName + " " + _clientModel.LastName);
            Console.WriteLine("Typ konta: " + _clientModel.AccountType);
            Console.WriteLine("ID klienta: " + _clientModel.Id);
            Console.WriteLine("Login: " + _clientModel.Username);
            Console.WriteLine("Hasło: " + _clientModel.Password);
            Console.WriteLine("Imię: " + _clientModel.FirstName);
            Console.WriteLine("Nazwisko: " + _clientModel.LastName);
            Console.WriteLine("Email: " + _clientModel.Email);
            Console.WriteLine("Telefon komórkowy: " + _clientModel.PhoneNumber);
        }
        public void SupplierRegistrationData(SupplierModel _supplierModel)
        {
            Console.Clear();
            Console.WriteLine("Zarejestrowano nowego dostawcę:");
            Console.WriteLine($"ID dostawcy: {_supplierModel.Id}");
            Console.WriteLine($"Typ konta: {_supplierModel.AccountType}");
            Console.WriteLine($"Stanowisko: {_supplierModel.Position}");
            Console.WriteLine($"Nazwa firmy: {_supplierModel.CompanyName}");
            Console.WriteLine($"Login: {_supplierModel.Username}");
            Console.WriteLine($"Imię: {_supplierModel.FirstName}");
            Console.WriteLine($"Nazwisko: {_supplierModel.LastName}");
            Console.WriteLine($"Email: {_supplierModel.Email}");
            Console.WriteLine($"Numer telefonu: {_supplierModel.PhoneNumber}");
        }
        public void EmployeeRegistrationData(EmployeeModel _employeeModel)
        {
            Console.Clear();
            Console.WriteLine("Zarejestrowano nowego pracownika:");
            Console.WriteLine($"ID pracownika: {_employeeModel.EmployeeId}");
            Console.WriteLine($"Typ konta: {_employeeModel.AccountType}");
            Console.WriteLine($"Login: {_employeeModel.Username}");
            Console.WriteLine($"Imię: {_employeeModel.FirstName}");
            Console.WriteLine($"Nazwisko: {_employeeModel.LastName}");
            Console.WriteLine($"Data urodzenia: {_employeeModel.DateOfBirth.ToShortDateString()}");
            Console.WriteLine($"Stanowisko: {_employeeModel.Position}");
        }
    }
}
