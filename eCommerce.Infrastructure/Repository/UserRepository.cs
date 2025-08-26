using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.IRepository;
using eCommerce.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _dapperDbContext;

        public UserRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }
        public async Task<ApplicationUser?> AddUser(ApplicationUser applicationUser)
        { 
            applicationUser.UserId = Guid.NewGuid();    
            string? query = "INSERT INTO public.\"Users\" VALUES(@UserId,@UserName,@Email,@Password,@Gender)";

            int rowsAffected=  await _dapperDbContext.DbConnection.ExecuteAsync(query, applicationUser);
            if (rowsAffected > 0)
                return applicationUser;
            else
                return null;
        }

        public async Task<ApplicationUser?> GetUserByEmailAndPassword(string email, string password)
        {

            string?query = "SELECT * FROM public.\"Users\" WHERE \"Email\"=@Email AND \"Password\" = @Password";
            var parameters = new { Email = email, Password = password };

            ApplicationUser?user= await _dapperDbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);
            if (user != null)
                return user;
            else
               return null;
        }
    }
}
