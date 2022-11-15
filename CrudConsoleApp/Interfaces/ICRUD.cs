using CrudConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudConsoleApp.Interfaces
{
    interface ICRUD
    {
        void Create<T>(ref T obj);
        bool Read<T>(out List<T> data);
        bool Update<T1,T2>(ref T1 obj, string attribute, ref T2 newValue);
        void Delete<T>(ref T obj);
    }
}
