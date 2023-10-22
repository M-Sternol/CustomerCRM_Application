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
    public class RegisterEmployee
    {
        public void RegistrationEmployee()
        {
            try
            {
                UserDocumentation UD = new UserDocumentation();
                EmployeeModel employee = new EmployeeModel();
                FileHandler save = new FileHandler();

                Console.WriteLine("Proces rejestracji pracownika..." + "\n");

                UD.DisplayLoginValidationHelp();
                string username;
                do
                {
                    Console.Write("Login: ");
                    username = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateUserLogin(username));
                employee.Username = username;

                UD.DisplayPasswordValidationHelp();
                string password;
                do
                {
                    Console.Write("Hasło: ");
                    password = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidatePassword(password));
                string hashedPassword = PasswordUtils.HashPassword(password);
                employee.Password = hashedPassword;

                UD.DisplayNameValidationHelp();
                string firstName;
                do
                {
                    Console.Write("Imię: ");
                    firstName = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateName(firstName));
                employee.FirstName = firstName;

                string lastName;
                do
                {
                    Console.Write("Nazwisko: ");
                    lastName = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateName(lastName));
                employee.LastName = lastName;

                bool isDateValid = false;
                DateTime dateOfBirth;
                do
                {
                    Console.Write("Data urodzenia (RRRR-MM-DD): ");
                    string input = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                    if (DateTime.TryParse(input, out dateOfBirth))
                    {
                        dateOfBirth = dateOfBirth.Date;
                        isDateValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Niepoprawny format daty. Podaj w formacie RRRR-MM-DD.");
                    }
                } while (!isDateValid);

                employee.DateOfBirth = dateOfBirth.Date;

                UD.DisplayPositionValidationHelp();
                string position;
                do
                {
                    Console.Write("Stanowisko: ");
                    position = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.IsPositionValid(position));
                employee.Position = position;

                UD.DisplayPhoneNumberValidationHelp();
                string phoneNumber;
                do
                {
                    Console.Write("Numer telefonu: ");
                    phoneNumber = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidatePhoneNumber(phoneNumber));
                employee.PhoneNumber = phoneNumber;

                UD.DisplayEmailValidationHelp();
                string email;
                do
                {
                    Console.Write("Email: ");
                    email = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                } while (!ValidationHelper.ValidateEmail(email));
                employee.Email = email;

                employee.EmployeeId = GenerateUniqueIdService.GenerateUniqueId();
                employee.AccountType = position;

                save.SaveEmployeeToJson(employee);
                save.SaveUserloginToJson(employee.EmployeeId, employee.AccountType, employee.Username, employee.Password);

                RegistrationData employeeRegistrationData = new RegistrationData();
                employeeRegistrationData.EmployeeRegistrationData(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Employee Registration: {ex.Message}");
                LogToFileMessage.LogError($"Error Employee Registration: {ex.Message}", ex.StackTrace);
            }
        }
    }
}
