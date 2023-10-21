
using Customer.Domain.Model.User.LoginModel;
using Customer.Domain.Model.User.UserModel;
using Customer.DataStorage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Customer.Application.Services.RegistrationService.SaveRegistrationToFile
{
    public class FileHandler
    {
        public void SaveCustomerToJson(ClientModel customer)
        {
            string filePath = FileLocations.GetCustomerFilePath();

            List<ClientModel> existingData = new List<ClientModel>();

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                existingData = JsonConvert.DeserializeObject<List<ClientModel>>(fileContent) ?? new List<ClientModel>();
            }

            existingData.Add(customer);

            string updatedContent = JsonConvert.SerializeObject(existingData, Formatting.Indented);
            File.WriteAllText(filePath, updatedContent);
        }
        public void SaveSupplierToJson(SupplierModel supplier)
        {
            string filePath = FileLocations.GetSupplierFilePath();

            List<SupplierModel> existingData = new List<SupplierModel>();

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                existingData = JsonConvert.DeserializeObject<List<SupplierModel>>(fileContent) ?? new List<SupplierModel>();
            }

            existingData.Add(supplier);

            string updatedContent = JsonConvert.SerializeObject(existingData, Formatting.Indented);
            File.WriteAllText(filePath, updatedContent);
        }
        public void SaveEmployeeToJson(EmployeeModel employee)
        {
            string filePath = FileLocations.GetEmployeeFilePath();

            List<EmployeeModel> existingData = new List<EmployeeModel>();

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                existingData = JsonConvert.DeserializeObject<List<EmployeeModel>>(fileContent) ?? new List<EmployeeModel>();
            }

            var employeeWithoutTime = new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                AccountType = employee.AccountType,
                Username = employee.Username,
                Password = employee.Password,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth.Date,
                Position = employee.Position,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email
            };

            existingData.Add(employeeWithoutTime);

            string updatedContent = JsonConvert.SerializeObject(existingData, Formatting.Indented,
                new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd" });
            File.WriteAllText(filePath, updatedContent);
        }



        public void SaveUserloginToJson(string id, string accountType, string userName, string password)
        {
            string filePath = FileLocations.GetUserLoginsFilePath();

            List<UserLogin> existingData = new List<UserLogin>();

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                existingData = JsonConvert.DeserializeObject<List<UserLogin>>(fileContent) ?? new List<UserLogin>();
            }

            var userLogin = new UserLogin
            {
                Id = id,
                AccountType = accountType,
                Username = userName,
                Password = password
            };

            existingData.Add(userLogin);

            string updatedContent = JsonConvert.SerializeObject(existingData, Formatting.Indented);
            File.WriteAllText(filePath, updatedContent);
        }
    }
}
