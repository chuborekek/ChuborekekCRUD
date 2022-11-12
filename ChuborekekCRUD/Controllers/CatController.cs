using ChuborekekCRUD.Common;
using ChuborekekCRUD.Data;
using ChuborekekCRUD.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChuborekekCRUD.Controllers
{
    public class CatController : Controller
    {
        private readonly ICatRepository _catRepo;

        public CatController(ICatRepository catRepo)
        {
            _catRepo = catRepo;
        }

        public async Task<IActionResult> Index(RecordsRequest recordsRequest)
        {
            var cats = await _catRepo.GetAllWithCriteria(recordsRequest);
            return View(cats);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cat cat)
        {
            if (ModelState.IsValid)
            {
                await _catRepo.Insert(cat);
                return RedirectToAction("Index");
            }
            return PartialView("_Create", cat);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cat cat)
        {
            if (!ModelState.IsValid) return PartialView("_Edit", cat);
            await _catRepo.Update(cat.Id, cat);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Cat cat)
        {
            await _catRepo.Delete(cat.Id);
            return RedirectToAction("Index");
        }
    }
}
