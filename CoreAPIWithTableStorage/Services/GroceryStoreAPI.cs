using CoreAPIWithTableStorage.Model;
using System.Threading.Tasks;

namespace CoreAPIWithTableStorage.Services
{
    
        public interface ITableStorageService
        {
            Task<GroceryItemEntity> RetrieveAsync(string category, string id);
            Task<GroceryItemEntity> InsertOrMergeAsync(GroceryItemEntity entity);
            Task<GroceryItemEntity> DeleteAsync(GroceryItemEntity entity);
        }
  
}
