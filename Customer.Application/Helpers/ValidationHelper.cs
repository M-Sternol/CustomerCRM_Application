using Customer.DataStorage;
using Customer.Domain.Model.User.LoginModel;
using Customer.Domain.Model.User.UserModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Customer.Application.Helpers
{
    public class ValidationHelper
    {
        public static bool registrationCancelled;
        public static bool ValidateUserLogin(string username)
        {
            string filePath = FileLocations.GetUserLoginsFilePath();
            List<UserLogin> userLogins = GetUserLogins(filePath);

            return !string.IsNullOrEmpty(username) &&
                   username.Length >= 4 &&
                   username.Length <= 14 &&
                   Regex.IsMatch(username, @"^[a-zA-Z0-9]+$") &&
                   Regex.IsMatch(username, @"^[a-zA-Z]*[0-9]+[a-zA-Z0-9]*$") &&
                   !userLogins.Any(u => u.Username == username);
        }

        private static List<UserLogin> GetUserLogins(string filePath)
        {
            List<UserLogin> existingData = new List<UserLogin>();

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                existingData = JsonConvert.DeserializeObject<List<UserLogin>>(fileContent) ?? new List<UserLogin>();
            }

            return existingData;
        }

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Length < 8 || password.Length > 16)
            {
                return false;
            }

            int specialCharCount = 0;

            foreach (char c in password)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    specialCharCount++;
                    if (specialCharCount > 1)
                    {
                        return false;
                    }
                }
            }

            if (specialCharCount == 0)
            {
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            return true;
        }



        public static bool ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            if (char.IsLower(name[0]))
            {
                name = char.ToUpper(name[0]) + name.Substring(1);
            }

            if (Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            if (!Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$"))
            {
                return false;
            }
            return true;
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            if (phoneNumber.Length != 9)
            {
                return false;
            }

            return true;
        }
        public static bool ValidateAddress(string address)
        {
            string[] parts = address.Split(' ');

            if (parts.Length < 2)
            {
                return false;
            }

            if (!IsAlpha(parts[0]))
            {
                return false;
            }
            if (!IsFraction(parts[1]))
            {
                return false;
            }

            return true;
        }
        public static bool IsAlpha(string input)
        {
            return input.All(char.IsLetter);
        }
        public static bool IsFraction(string input)
        {
            string[] fractionParts = input.Split('/');
            if (fractionParts.Length != 2)
            {
                return false;
            }

            return fractionParts[0].All(char.IsDigit) && fractionParts[1].All(char.IsDigit);
        }

        public static bool ValidateText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            if (Regex.IsMatch(text, @"[^a-zA-Z\s]"))
            {
                return false;
            }

            return true;
        }
        public static bool IsPositionValid(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
            {
                return false;
            }

            position = position.ToUpper();

            string[] validPositions = { "MANAGER", "KIEROWNIK", "SPECJALISTA", "ASYSTENT", "PRACOWNIK" };

            if (!validPositions.Contains(position))
            {
                Console.WriteLine("Nieprawidłowe stanowisko. Dostępne stanowiska to: Manager, Kierownik, Specjalista, Asystent, Pracownik.");
                return false;
            }

            string filePath = FileLocations.GetEmployeeFilePath();
            List<EmployeeModel> employees = GetEmployees(filePath);

            if (position == "ADMINISTRATOR")
            {
                if (employees.Any(e => e.AccountType == "Administrator"))
                {
                    Console.WriteLine("Może być tylko jeden Administrator. Nie można zarejestrować więcej niż jednego administratora.");
                    return false;
                }
            }

            return true;
        }

        private static List<EmployeeModel> GetEmployees(string filePath)
        {
            List<EmployeeModel> existingData = new List<EmployeeModel>();

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                existingData = JsonConvert.DeserializeObject<List<EmployeeModel>>(fileContent) ?? new List<EmployeeModel>();
            }

            return existingData;
        }




    }
}

