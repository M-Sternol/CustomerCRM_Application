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
    public class DataChangeSupplier
    {
        private List<SupplierModel> _supplier;
        private List<UserLogin> _userLogins;

        public DataChangeSupplier()
        {
            LoadData();
        }

        private void LoadData()
        {
            var supplierJson = File.ReadAllText(FileLocations.GetSupplierFilePath());
            _supplier = JsonConvert.DeserializeObject<List<SupplierModel>>(supplierJson);

            var userLoginJson = File.ReadAllText(FileLocations.GetUserLoginsFilePath());
            _userLogins = JsonConvert.DeserializeObject<List<UserLogin>>(userLoginJson);
        }

        public UserLogin GetSupplierLogin(string username)
        {
            return _userLogins.FirstOrDefault(u => u.Username == username);
        }

        public void UpdateSupplierUsername(string supplierId, string newUsername)
        {
            var supplierToUpdate = _supplier.FirstOrDefault(c => c.Id == supplierId);
            if (supplierToUpdate != null)
            {
                supplierToUpdate.Username = newUsername;
                SaveSupplierData();
                UpdateUserLoginData(supplierId, newUsername, supplierToUpdate.Password);
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }

        public void UpdateSupplierPassword(string supplierId)
        {
            var supplierToUpdate = _supplier.FirstOrDefault(c => c.Id == supplierId);
            if (supplierToUpdate != null)
            {
                var newPassword = PasswordUtils.GetMaskedPassword();
                var hashedPassword = PasswordUtils.HashPassword(newPassword);
                supplierToUpdate.Password = hashedPassword;
                SaveSupplierData();
                UpdateUserLoginData(supplierId, supplierToUpdate.Username, hashedPassword);
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }

        public void UpdateSupplierFirstName(string supplierId, string newFirstName)
        {
            var supplierToUpdate = _supplier.FirstOrDefault(c => c.Id == supplierId);
            if (supplierToUpdate != null)
            {
                supplierToUpdate.FirstName = newFirstName;
                SaveSupplierData();
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }

        public void UpdateSupplierLastName(string supplierId, string newLastName)
        {
            var supplierToUpdate = _supplier.FirstOrDefault(c => c.Id == supplierId);
            if (supplierToUpdate != null)
            {
                supplierToUpdate.LastName = newLastName;
                SaveSupplierData();
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }

        public void UpdateSupplierEmail(string supplierId, string newEmail)
        {
            var supplierToUpdate = _supplier.FirstOrDefault(c => c.Id == supplierId);
            if (supplierToUpdate != null)
            {
                supplierToUpdate.Email = newEmail;
                SaveSupplierData();
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }

        public void UpdateSupplierPhoneNumber(string supplierId, string newPhoneNumber)
        {
            var supplierToUpdate = _supplier.FirstOrDefault(c => c.Id == supplierId);
            if (supplierToUpdate != null)
            {
                supplierToUpdate.PhoneNumber = newPhoneNumber;
                SaveSupplierData();
            }
            else
            {
                Console.WriteLine("Nie znaleziono klienta o podanym identyfikatorze.");
            }
        }
        private void SaveSupplierData()
        {
            var updatedSupplierJson = JsonConvert.SerializeObject(_supplier, Formatting.Indented);
            File.WriteAllText(FileLocations.GetSupplierFilePath(), updatedSupplierJson);
        }

        private void UpdateUserLoginData(string supplierId, string newUsername, string newPassword)
        {
            var userLoginToUpdate = _userLogins.FirstOrDefault(u => u.Id == supplierId);

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
