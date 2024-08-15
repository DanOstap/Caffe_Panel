using Caffe_Panel.Models;
using Microsoft.EntityFrameworkCore;

namespace Caffe_Panel.DataBase.Service
{
    public interface IUsersService
    {
        public Task<User> CreateUser(User model);
        public Task<List<User>?> FindAll();
        public Task<User?> FindOneById(int id);
        public Task<User?> FindOneByName(string name);
        public Task<User?> Update(int id, User model);
        public Task<User?> RemoveById(int id);
        public Task<User?> RemoveByName(string name);
    }

    public class UserService : IUsersService
    {
       private readonly Configurations _configuration;
        public UserService(Configurations configuration)
        {
            _configuration = configuration;
        }
        public async Task<User> CreateUser(User model) {
           var user = await _configuration.User.AddAsync(model);
            await _configuration.SaveChangesAsync();
            return model;
        }
        public async Task<List<User>> FindAll() {
            if (_configuration.User == null) return null;
            return await _configuration.User.ToListAsync();
        }
        public async Task<User> FindOneById(int id) {
            var user = await _configuration.User.FindAsync(id);
            if (user != null) return user;
            return null;
        }
        public async Task<User> FindOneByName(string name) {
            var user = await _configuration.User.Where(i => i.Name_Subname == name).FirstOrDefaultAsync();
            if (user != null) return user;
            return null;
        }
        public async Task<User> Update(int id, User model) {
            var user = await _configuration.User.FindAsync(id);
            if (user != null) {
                _configuration.Entry(model).State = EntityState.Modified;
                try {
                    await _configuration.SaveChangesAsync();
                    return model;
                }
                catch (DbUpdateConcurrencyException err) {
                    Console.WriteLine(err);
                    return null;
                }
           }
            return null;
        }
        public async  Task<User> RemoveById(int id) {
            var user = await _configuration.User.FindAsync(id);
            if (user != null) {
                _configuration.User.Remove(user);
                return user;
            }
            return null;
        }
        public async Task<User> RemoveByName(string name) {
            var user = await _configuration.User.Where(i => i.Name_Subname == name).FirstOrDefaultAsync();
            if (user != null)
            {
                _configuration.User.Remove(user);
                return user;
            }
            return null;
        }
    }
}
