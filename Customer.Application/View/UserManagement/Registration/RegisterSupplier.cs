using Customer.Application.Helpers;
using Customer.Application.Helpers.Utils;
using Customer.Application.Services.RegistrationService;
using Customer.Application.Services.RegistrationService.SaveRegistrationToFile;
using Customer.Application.Services.Security;
using Customer.DataStorage;
using Customer.Domain.Model.User.UserModel;
using System;

namespace Customer.Application.View.UserManagement.Registration
{
    public class RegisterSupplier
    {
        public void RegistrationSupplier()
        {
            try 
            {
                UserDocumentation UD = new UserDocumentation();
                SupplierModel supplier = new SupplierModel();
                FileHandler save = new FileHandler();

                Console.WriteLine("Proces rejestracji dostawcy..." + "\n");

                UD.DisplayLoginValidationHelp();
                string username;
                do
                {
                    Console.Write("Login: ");
                    username = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateUserLogin(username));
                supplier.Username = username;

                UD.DisplayPasswordValidationHelp();
                string password;
                do
                {
                    Console.Write("Hasło: ");
                    password = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidatePassword(password));
                string hashedPassword = PasswordUtils.HashPassword(password);
                supplier.Password = hashedPassword;

                UD.DisplayNameValidationHelp();
                string firstName;
                do
                {
                    Console.Write("Imię: ");
                    firstName = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateName(firstName));
                supplier.FirstName = firstName;

                string lastName;
                do
                {
                    Console.Write("Nazwisko: ");
                    lastName = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateName(lastName));
                supplier.LastName = lastName;

                UD.DisplayEmailValidationHelp();
                string email;
                do
                {
                    Console.Write("Email: ");
                    email = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateEmail(email));
                supplier.Email = email;

                UD.DisplayPhoneNumberValidationHelp();
                string phoneNumber;
                do
                {
                    Console.Write("Numer telefonu: ");
                    phoneNumber = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidatePhoneNumber(phoneNumber));
                supplier.PhoneNumber = phoneNumber;

                UD.DisplayPositionValidationHelp();
                string position;
                do
                {
                    Console.Write("Stanowisko: ");
                    position = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.IsPositionValid(position));
                supplier.Position = position;

                
                string companyName;
                do
                {
                    Console.Write("Nazwa firmy: ");
                    companyName = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateText(companyName));
                supplier.CompanyName = companyName;

                supplier.Id = GenerateUniqueIdService.GenerateUniqueId();
                supplier.AccountType = "Dostawca";

                save.SaveSupplierToJson(supplier);
                save.SaveUserloginToJson(supplier.Id, supplier.AccountType, supplier.Username, supplier.Password);

                RegistrationData supplierRegistrationData = new RegistrationData();
                supplierRegistrationData.SupplierRegistrationData(supplier);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Supplier Registration: {ex.Message}");
                LogToFileMessage.LogError($"Error Supplier Registration: {ex.Message}", ex.StackTrace);
            }
        }
    }
}
