using Customer.Application.Interfaces;
using Customer.Application.Services.Security;
using Customer.Application.Services.LoginsSystemService;
using Customer.Application.View.User;
using Customer.Domain.Model.User.LoginModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.DataStorage;
using Customer.Domain.Interfaces;
using Customer.Application.Helpers.Utils;
using Customer.Application.Helpers;

namespace Customer.Application.View.UserManagement.Login
{
    public class LoginSystem
    {
        public UserLogin loggedInUser;
        LoginRedirect redirect = new LoginRedirect();
        private readonly Title title = new Title();

        public void LoginsSystem()
        {
            try
            {
                bool isLoggedIn = false;
                while (!isLoggedIn)
                {
                    title.ViewTitle();
                    Console.Write("Wprowadź nazwę użytkownika: ");
                    string username = Console.ReadLine();
                    Console.Write("Wprowadź hasło: ");
                    string password = PasswordUtils.GetMaskedPassword();

                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    {
                        string filePath = FileLocations.GetUserLoginsFilePath();
                        List<UserLogin> userLogins = GetUserLogins(filePath);

                        foreach (var user in userLogins)
                        {
                            if (user.Username == username && PasswordUtils.VerifyPassword(password, user.Password))
                            {
                                isLoggedIn = true;
                                Console.Clear();
                                loggedInUser = user;
                                redirect.RedirectUser(loggedInUser.AccountType, loggedInUser.Username);
                                break;
                            }
                        }

                        if (!isLoggedIn)
                        {
                            Console.WriteLine("Nieprawidłowa nazwa użytkownika lub hasło. Spróbuj ponownie. albo wyjść wciśnij - ESC");
                            string esc = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowa nazwa użytkownika lub hasło. Spróbuj ponownie. albo wyjść wciśnij - ESC");
                        string esc = CheckForEscKey.ReadInput(ref ValidationHelper.registrationCancelled);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Loggin: {ex.Message}");
                LogToFileMessage.LogError($"Error Loggin: {ex.Message}", ex.StackTrace);
            }
        }

        private List<UserLogin> GetUserLogins(string filePath)
        {
            List<UserLogin> existingData = new List<UserLogin>();

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                existingData = JsonConvert.DeserializeObject<List<UserLogin>>(fileContent) ?? new List<UserLogin>();
            }

            return existingData;
        }
    }
}
