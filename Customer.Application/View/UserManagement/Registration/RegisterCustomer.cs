using Customer.Application.Helpers;
using Customer.Application.Helpers.Utils;
using Customer.Application.Services.RegistrationService.SaveRegistrationToFile;
using Customer.Application.Services.RegistrationService;
using Customer.Application.Services.Security;
using Customer.Domain.Model.User.UserModel;
using Customer.DataStorage;
using System;

namespace Customer.Application.View.UserManagement.Registration
{
    public class RegisterCustomer
    {
        public void RegistrationCustomer()
        {
            try
            {
                UserDocumentation UD = new UserDocumentation();
                ClientModel client = new ClientModel();
                FileHandler save = new FileHandler();

                Console.WriteLine("Proces rejestracji klienta..." + "\n");

                UD.DisplayLoginValidationHelp();
                string username;
                do
                {
                    Console.Write("Login: ");
                    username = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateUserLogin(username));
                client.Username = username;

                UD.DisplayPasswordValidationHelp();
                string password;
                do
                {
                    Console.Write("Hasło: ");
                    password = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidatePassword(password));
                string hashedPassword = PasswordUtils.HashPassword(password);
                client.Password = hashedPassword;

                UD.DisplayNameValidationHelp();
                string firstName;
                do
                {
                    Console.Write("Imię: ");
                    firstName = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateName(firstName));
                client.FirstName = firstName;

                string lastName;
                do
                {
                    Console.Write("Nazwisko: ");
                    lastName = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateName(lastName));
                client.LastName = lastName;

                UD.DisplayEmailValidationHelp();
                string email;
                do
                {
                    Console.Write("Email: ");
                    email = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateEmail(email));
                client.Email = email;
                
                UD.DisplayPhoneNumberValidationHelp();
                string phoneNumber;
                do
                {
                    Console.Write("Telefon komórkowy: ");
                    phoneNumber = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidatePhoneNumber(phoneNumber));
                client.PhoneNumber = phoneNumber;

                client.Id = GenerateUniqueIdService.GenerateUniqueId();
                client.AccountType = "Klient";

                save.SaveCustomerToJson(client);
                save.SaveUserloginToJson(client.Id, client.AccountType, client.Username, client.Password);

                RegistrationData registrationData = new RegistrationData();
                registrationData.CustomerRegistrationData(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Customer Registration: {ex.Message}");
                LogToFileMessage.LogError($"Error Customer Registration: {ex.Message}", ex.StackTrace);
            }
        }

    }
}

