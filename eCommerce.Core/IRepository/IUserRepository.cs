using eCommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.IRepository
{
     public interface  IUserRepository
     {

          Task<ApplicationUser?>AddUser(ApplicationUser applicationUser);   

          Task<ApplicationUser?>GetUserByEmailAndPassword(string email, string password);   
     }
}
