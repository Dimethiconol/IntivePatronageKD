using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IntivePatronageKD.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntivePatronageKD.Services
{
    public interface IUserService
    {
        public int Create(User user);
        IEnumerable<User> GetAll();
        public User GetUser(int id);
        public bool Update(int id, User user);
        public bool Delete(int id);
    }



    public class UserServiece : IUserService
    {

        private readonly UsersDbContext _dbContext;

        public UserServiece(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }




        public int Create(User user)
        {
            var newUser = user;
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return newUser.Id;
        }



        public IEnumerable<User> GetAll()
        {
            var users = _dbContext
                .Users
                .Include(u => u.Address)
                .ToList();

            return users;
        }



        public User GetUser(int id)
        {
            var user = _dbContext
                .Users
                .Include(u => u.Address)
                .FirstOrDefault(u => u.Id == id);

            if (user is null) return null;

            return user;
        }




        public bool Update(int id, User user)
        {
            var updatedUser = _dbContext
                .Users
                .Include(u => u.Address)
                .FirstOrDefault(u => u.Id == id);

            if (updatedUser is null) return false;

            foreach (PropertyInfo prop in updatedUser.GetType().GetProperties())
            {
                if (prop.Name == "Id" || prop.Name == "AddressId")
                {
                    continue;
                }

                if (prop.GetValue(user) != null)
                {
                    if (prop.Name == "Address")
                    {
                        foreach (PropertyInfo prop_adr in updatedUser.Address.GetType().GetProperties())
                        {
                            
                            if (prop_adr.Name == "Id")
                            {
                                continue;
                            }
                            if (prop_adr.GetValue(user.Address) != null)
                            {
                                prop_adr.SetValue(updatedUser.Address, prop_adr.GetValue(user.Address));
                            }
                        }
                    }
                    else
                    {
                        prop.SetValue(updatedUser, prop.GetValue(user));
                    }
                }
            }


            _dbContext.SaveChanges();
            return true;
        }



        public bool Delete(int id)
        {
            var deletedUser = _dbContext
                .Users
                .Include(u => u.Address)
                .FirstOrDefault(u => u.Id == id);

            if (deletedUser is null) return false;

            _dbContext.Users.Remove(deletedUser);
            _dbContext.Addresses.Remove(deletedUser.Address);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
