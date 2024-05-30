using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDSExamNet7.Data;
using SDSExamNet7.Models.DTOS;
using SDSExamNet7.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SDSExamNet7.Controllers
{
    public class RecyclableItemController : Controller
    {
        private readonly AppDbContext dbContext_;

        public RecyclableItemController(AppDbContext dbContext)
        {
            dbContext_ = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> AddRecyclableItem()
        {
            var model = new AddRecyclableItemDTO
            {
                RecyclableTypes = await dbContext_.RecyclableType.ToListAsync(),
                RecyclableItem = new RecyclableItemWithTypeDTO()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecyclableItem(AddRecyclableItemDTO recyclableItemDTO)
        {
            try
            {
                var recyclableItem = new RecyclableItem
                {
                    RecyclableTypeId = recyclableItemDTO.RecyclableItem.RecyclableTypeId,
                    Weight = recyclableItemDTO.RecyclableItem.Weight,
                    ComputedRate = recyclableItemDTO.RecyclableItem.ComputedRate,
                    ItemDescription = recyclableItemDTO.RecyclableItem.ItemDescription
                };

                dbContext_.RecyclableItem.Add(recyclableItem);
                await dbContext_.SaveChangesAsync();

                return RedirectToAction("RecyclableItemList", "RecyclableItem");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> RecyclableItemList()
        {
            var items = await dbContext_.RecyclableItem
                .Include(ri => ri.RecyclableType)
                .Select(ri => new RecyclableItemWithTypeDTO
                {
                    Id = ri.Id,
                    Weight = ri.Weight,
                    ComputedRate = ri.ComputedRate,
                    ItemDescription = ri.ItemDescription,
                    RecyclableTypeId = ri.RecyclableTypeId,
                    Type = ri.RecyclableType.Type

                }).ToListAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> EditRecyclableItem(int id)
        {
            var recyclableType = await dbContext_.RecyclableType.FindAsync(id);
            return View(recyclableType);
        }
        [HttpPost]
        public async Task<IActionResult> EditRecyclableItem(RecyclableItem recyclableItemModel)
        {
            var recyclableItem = await dbContext_.RecyclableItem.FindAsync(recyclableItemModel.Id);
            if (recyclableItem is not null)
            {
                recyclableItem.RecyclableTypeId = recyclableItemModel.RecyclableTypeId;
                recyclableItem.Weight = recyclableItemModel.Weight;
                recyclableItem.ComputedRate = recyclableItemModel.ComputedRate;
                recyclableItem.ItemDescription = recyclableItemModel.ItemDescription;

                await dbContext_.SaveChangesAsync();
                return RedirectToAction("RecyclableItemList", "RecyclableItem");
            }

            return View(recyclableItemModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecyclableItem(RecyclableItem recyclableItemModel)
        {
            RecyclableItem recyclableItem = await dbContext_.RecyclableItem
                .FirstOrDefaultAsync(p => p.Id == recyclableItemModel.Id);

            if (recyclableItem is not null)
            {
                dbContext_.RecyclableItem.Remove(recyclableItem);
                await dbContext_.SaveChangesAsync();
            }

            return RedirectToAction("RecyclableItemList", "RecyclableItem");
        }


    }
}
