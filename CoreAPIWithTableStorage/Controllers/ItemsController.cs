using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CoreAPIWithTableStorage.Model;
using CoreAPIWithTableStorage.Services;

namespace CoreAPIWithTableStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController  : ControllerBase
    {
        private readonly ITableStorageService _storageService;
        public ItemsController(ITableStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }
        [HttpGet]
        [ActionName(nameof(GetAsync))]
        public async Task<IActionResult> GetAsync([FromQuery] string category, string id)
        {
            return Ok(await _storageService.RetrieveAsync(category, id));
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] GroceryItemEntity entity)
        {
            entity.PartitionKey = entity.Category;
            string Id = Guid.NewGuid().ToString();
            entity.Id = Id;
            entity.RowKey = Id;
            var createdEntity = await _storageService.InsertOrMergeAsync(entity);
            return CreatedAtAction(nameof(GetAsync), createdEntity);
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] GroceryItemEntity entity)
        {
            entity.PartitionKey = entity.Category;
            entity.RowKey = entity.Id;
            await _storageService.InsertOrMergeAsync(entity);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] string category, string id)
        {
            var entity = await _storageService.RetrieveAsync(category, id);
            await _storageService.DeleteAsync(entity);
            return NoContent();
        }
    }
}
