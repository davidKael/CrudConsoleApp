using CrudConsoleApp.Db;
using CrudConsoleApp.Interfaces;
using CrudConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudConsoleApp.Controllers
{
    class UsersController : IUser
    {
        private readonly KunskapsProvDbContext _context = new KunskapsProvDbContext();

        public void Create(User user)
        {
            _context.Add(user);
            _context.SaveChanges();           
        }

        public void Delete(User user)
        {          
            _context.Remove(user);
            _context.SaveChanges();
        }

        public List<User> Read()
        {
            return _context.Users.ToList();
        }

        public bool Update(User user, string attribute, string newValue)
        {
            switch (attribute)
            {
                case "First Name":
                    user.FirstName = newValue;
                    break;
                case "Last Name":
                    user.LastName = newValue;
                    break;
                case "Email":
                    user.Email = newValue;
                    break;
                case "Address":
                    user.Address = newValue;
                    break;
                default:
                    return false;

            }
            _context.SaveChanges();
            return true;
            
        }
    }
}
