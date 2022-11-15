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
    class UsersController : ICRUD
    {
        private readonly KunskapsProvDbContext _context = new KunskapsProvDbContext();

        public void Create<T>(ref T user)
        {
            
            _context.Add(user);
            _context.SaveChanges();           
        }

        public void Delete<T>(ref T user)
        {          
            _context.Remove(user);
            _context.SaveChanges();
        }

        public bool Read<T>(out List<T> users)
        {
            users = _context.Users.ToList() as List<T>;
            
            return users != null;
        }

        public bool Update<T1,T2>(ref T1 obj, string attribute, ref T2 newValue)
        {
           User user= obj as User;
            switch (attribute)
            {
                case "First Name":
                    user.FirstName = newValue as string;
                    break;
                case "Last Name":
                    user.LastName = newValue as string;
                    break;
                case "Email":
                    user.Email = newValue as string;
                    break;
                case "Address":
                    user.Address = newValue as string;
                    break;
                default:
                    return false;

            }
            _context.SaveChanges();
            return true;
            
        }
    }
}
