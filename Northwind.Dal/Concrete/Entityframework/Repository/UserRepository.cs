using Microsoft.EntityFrameworkCore;
using Northwind.Dal.Abstract;
using Northwind.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Dal.Concrete.Entityframework.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }

        public User Login(User login)
        {
            //var user = dbset.Where(x => x.UserCode == login.UserCode && x.Password == login.Password).SingleOrDefault();

            var user = dbset.FirstOrDefault(x => x.UserCode == login.UserCode && x.Password == MD5Hash(login.Password) );

            //var user = dbset.SingleOrDefault(x => x.UserCode == login.UserCode && x.Password == login.Password);

            return user;
        }


        public User Register(User register)
        {
            User user = dbset.SingleOrDefault(x => x.UserCode == register.UserCode);
            if (user is null)
            {
                register.Password = MD5Hash(register.Password);
                dbset.Add(register);
                context.SaveChanges();
                return register;

            }
            else
            {
                throw new InvalidOperationException("User already registered");
            }


        }

        public string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }






        public void SeedUser(User newUser)
        {

            //ResetUsers();
            if (dbset.Any()) return;

            newUser.Password = MD5Hash(newUser.Password);

            dbset.Add(newUser);
            context.SaveChanges();

        }

        public void ResetUsers()
        {
            foreach(var item in dbset)
            {
                dbset.Remove(item);
                
            }
            context.SaveChanges();

        }




    }
}
