using CrudConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudConsoleApp.Interfaces
{
    interface IUser
    {
        void Create(User user);
        List<User> Read();
        bool Update(User user, string attribute, string newValue);
        void Delete(User user);
    }
}
