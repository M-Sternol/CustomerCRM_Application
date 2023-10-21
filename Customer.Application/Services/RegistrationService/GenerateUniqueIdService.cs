using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Services.RegistrationService
{
    public class GenerateUniqueIdService
    {
        public static string GenerateUniqueId() 
        {
            string guid = Guid.NewGuid().ToString();
            string shortId = guid.Substring(0, 8);

            return shortId;
        }
    }
}
