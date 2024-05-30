using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDSExamNet7.Data;
using SDSExamNet7.Models;
using SDSExamNet7.Models.Entities;

namespace SDSExamNet7.Controllers
{
    public class RecyclableTypeController : Controller
    {
        private readonly AppDbContext dbContext_;

        public RecyclableTypeController(AppDbContext dbContext)
        {
            dbContext_ = dbContext;
        }

        [HttpGet]
        public IActionResult AddRecyclableType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRecyclableType(AddRecyclableTypeViewModel recyclableTypeviewModel)
        {
            var AddRecyclableTypeView = new RecyclableType
            {
                Type = recyclableTypeviewModel.Type,
                Rate = recyclableTypeviewModel.Rate,
                MinKg = recyclableTypeviewModel.MinKg,
                MaxKg = recyclableTypeviewModel.MaxKg
            };

            await dbContext_.RecyclableType.AddAsync(AddRecyclableTypeView);
            await dbContext_.SaveChangesAsync();
            return RedirectToAction("RecyclableTypeList", "RecyclableType");

        }

        [HttpGet]
        public async Task<IActionResult> RecyclableTypeList()
        {

            var recyclableType = await dbContext_.RecyclableType.ToListAsync();
            return View(recyclableType);
        }
        [HttpGet]
        public async Task<IActionResult> EditRecyclableType(int id)
        {
            var recyclableType = await dbContext_.RecyclableType.FindAsync(id);
            return View(recyclableType);
        }

        [HttpPost]
        public async Task<IActionResult> EditRecyclableType(RecyclableType recyclableTypeModel)
        {
            var recyclableType = await dbContext_.RecyclableType.FindAsync(recyclableTypeModel.Id);
            if (recyclableType is not null)
            {
                recyclableType.Type = recyclableTypeModel.Type;
                recyclableType.Rate = recyclableTypeModel.Rate;
                recyclableType.MinKg = recyclableTypeModel.MinKg;
                recyclableType.MaxKg = recyclableTypeModel.MaxKg;
                await dbContext_.SaveChangesAsync();
            }
            return RedirectToAction("RecyclableTypeList", "RecyclableType");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecyclableType(RecyclableType recyclableTypeModel)
        {
            RecyclableType recyclableType = await dbContext_.RecyclableType
                .FirstOrDefaultAsync(p => p.Id == recyclableTypeModel.Id);

            if (recyclableType is not null)
            {
                dbContext_.RecyclableType.Remove(recyclableType); 
                await dbContext_.SaveChangesAsync();
            }

            return RedirectToAction("RecyclableTypeList", "RecyclableType");
        }


    }
}
