using Caffe_Panel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Caffe_Panel.DataBase.Service
{
    public interface IItemsService
    {
        public Task<Items> CreateItem(Items model);
        public Task<List<Items>?> FindAll();
        public Task<Items?> FindOneById(int id);
        public Task<Items?> FindOneByDiscount(int Discount_From, int Discount_To);
        public Task<Items?> FindOneByName(string name);
        public Task<User?> Update(int id, Items model);
        public Task<Items?> RemoveById(int id);
        public Task<Items?> RemoveByName(string name);
        
    }

    public class ItemsService
    {
        private readonly Configurations _configuration;
        public ItemsService(Configurations configuration)
        {
            _configuration = configuration;
        }
        public async Task<Items> CreateItem(Items model)
        {
            var item = await _configuration.Items.AddAsync(model);
            await _configuration.SaveChangesAsync();
            return model;
        }
        public async Task<List<Items>> FindAll()
        {
            if (_configuration.Items == null) return null;
            return await _configuration.Items.ToListAsync();
        }
        public async Task<Items> FindOneById(int id)
        {
            var item = await _configuration.Items.FindAsync(id);
            if (item != null) return item;
            return null;
        }
        public async Task<List<Items>> FindOneByDiscount(int Discount_From, int Discount_To)
        {
            if (Discount_From != null && Discount_To != null)
            {
                if (Discount_From > Discount_To)
                {

                    var items = await _configuration.Items
                        .Where(i => i.Product_Discount > Discount_From &&
                                    i.Product_Discount < Discount_To)
                        .ToListAsync();
                    return items;
                }
                if (Discount_From < Discount_To) {
                    var items = await _configuration.Items
                           .Where(i => i.Product_Discount > Discount_To &&
                                       i.Product_Discount < Discount_From)
                           .ToListAsync();
                    return items;
                }
                if (Discount_To == Discount_From) {
                    var items = await _configuration.Items
                              .Where(i => i.Product_Discount == Discount_To)
                              .ToListAsync();
                    return items;
                }
            }
            return null;
        }
        public async Task<Items> FindOneByName(string name)
        {
            var item = await _configuration.Items.Where(i => i.Product_Name == name).FirstOrDefaultAsync();
            if (item != null) return item;
            return null;
        }
        public async Task<Items> Update(int id, Items model)
        {
            var item = await _configuration.Items.FindAsync(id);
            if (item != null)
            {
                _configuration.Entry(model).State = EntityState.Modified;
                try
                {
                    await _configuration.SaveChangesAsync();
                    return model;
                }
                catch (DbUpdateConcurrencyException err)
                {
                    Console.WriteLine(err);
                    return null;
                }
            }
            return null;
        }
        public async Task<Items> RemoveById(int id) {
            var item = await _configuration.Items.FindAsync(id);
            if (item!= null)
            {
                _configuration.Items.Remove(item);
                return item;
            }
            return null;
        }
        public async Task<Items> RemoveByName(string name) {
            var item = await _configuration.Items.Where(i => i.Product_Name == name).FirstOrDefaultAsync();
            if (item != null)
            {
                _configuration.Items.Remove(item);
                return item;
            }
            return null;
        }
    }
}
