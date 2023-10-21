using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Customer.Domain.Model.User;
using Customer.Domain.Model.User.LoginModel;
using Customer.Domain.Model.User.UserModel;
using Customer.Application.Services.Security;
using Customer.DataStorage;

namespace Customer.Application.Services.User
{
    internal class DataChangeCustomer
    {
        private List<ClientModel> _customers;
        private List<UserLogin> _userLogins;

        public DataChangeCustomer()
        {
            LoadData();
        }

        private void LoadData()
        {
            var customerJson = File.ReadAllText(FileLocations.GetCustomerFilePath());
            _customers = JsonConvert.DeserializeObject<List<ClientModel>>(customerJson);

            var userLoginJson = File.ReadAllText(FileLocations.GetUserLoginsFilePath());
            _userLogins = JsonConvert.DeserializeObject<List<UserLogin>>(userLoginJson);
        }

        public UserLogin GetClientLogin(string username)
        {
            return _userLogins.FirstOrDefault(u => u.Username == username);
        }

        public void UpdateClientUsername(string clientId, string newUsername)
        {
            var clientToUpdate = _customers.FirstOrDefault(c => c.Id == clientId);
            if (clientToUpdate != null)
            {
                clientToUpdate.Username = newUsername;
                SaveCustomerData();
                UpdateUserLoginData(clientId, newUsername, clientToUpdate.Password);
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }

        public void UpdateClientPassword(string clientId)
        {
            var clientToUpdate = _customers.FirstOrDefault(c => c.Id == clientId);
            if (clientToUpdate != null)
            {
                var newPassword = PasswordUtils.GetMaskedPassword();
                var hashedPassword = PasswordUtils.HashPassword(newPassword);
                clientToUpdate.Password = hashedPassword;
                SaveCustomerData();
                UpdateUserLoginData(clientId, clientToUpdate.Username, hashedPassword);
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }

        public void UpdateClientFirstName(string clientId, string newFirstName)
        {
            var clientToUpdate = _customers.FirstOrDefault(c => c.Id == clientId);
            if (clientToUpdate != null)
            {
                clientToUpdate.FirstName = newFirstName;
                SaveCustomerData();
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }

        public void UpdateClientLastName(string clientId, string newLastName)
        {
            var clientToUpdate = _customers.FirstOrDefault(c => c.Id == clientId);
            if (clientToUpdate != null)
            {
                clientToUpdate.LastName = newLastName;
                SaveCustomerData();
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }

        public void UpdateClientEmail(string clientId, string newEmail)
        {
            var clientToUpdate = _customers.FirstOrDefault(c => c.Id == clientId);
            if (clientToUpdate != null)
            {
                clientToUpdate.Email = newEmail;
                SaveCustomerData();
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }

        public void UpdateClientPhoneNumber(string clientId, string newPhoneNumber)
        {
            var clientToUpdate = _customers.FirstOrDefault(c => c.Id == clientId);
            if (clientToUpdate != null)
            {
                clientToUpdate.PhoneNumber = newPhoneNumber;
                SaveCustomerData();
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }
        private void SaveCustomerData()
        {
            var updatedCustomerJson = JsonConvert.SerializeObject(_customers, Formatting.Indented);
            File.WriteAllText(FileLocations.GetCustomerFilePath(), updatedCustomerJson);
        }

        private void UpdateUserLoginData(string clientId, string newUsername, string newPassword)
        {
            var userLoginToUpdate = _userLogins.FirstOrDefault(u => u.Id == clientId);

            if (userLoginToUpdate != null)
            {
                userLoginToUpdate.Username = newUsername;
                userLoginToUpdate.Password = newPassword;

                var updatedUserLoginJson = JsonConvert.SerializeObject(_userLogins, Formatting.Indented);
                File.WriteAllText(FileLocations.GetUserLoginsFilePath(), updatedUserLoginJson);
            }
        }
    }
}
