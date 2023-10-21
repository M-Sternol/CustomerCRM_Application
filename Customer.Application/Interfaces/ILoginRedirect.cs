using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Interfaces
{
    public interface ILoginRedirect
    {
        void RedirectUser(string accountType,string username);
    }
}
