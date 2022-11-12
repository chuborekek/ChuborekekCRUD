using ChuborekekCRUD.Common;
using ChuborekekCRUD.Data;
using ChuborekekCRUD.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChuborekekCRUD.Controllers
{
    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepo;
        public DogController(IDogRepository dogrepo)
        {
            _dogRepo = dogrepo;
        }
        public async Task<IActionResult> Index(RecordsRequest recordsRequest)
        {
            var dogs = await _dogRepo.GetAllWithCriteria(recordsRequest);
            return View(dogs);
        }
        public async Task<IActionResult> Create()
        {
            return View(new Dog());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dog dog)
        {
            if(!ModelState.IsValid) 
            {
                return View(dog);
            }
            await _dogRepo.Insert(dog);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var dog = await _dogRepo.Get(r => r.Id == id);
            if (dog == null) return RedirectToAction("Index");
            return View(dog);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var dog = await _dogRepo.Get(r => r.Id == id);
            if (dog == null) return RedirectToAction("Index");
            return View(dog);
        }
        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> Edit(Dog dog)
        {
            if (!ModelState.IsValid) return View(dog);
            await _dogRepo.Update(dog.Id,dog);
            return RedirectToAction("Index");
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            var dog = await _dogRepo.Get(r => r.Id == id);
            if (dog == null) return RedirectToAction("Index");
            return View(dog);
        }
        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> Delete(Dog dog)
        {
            await _dogRepo.Delete(dog.Id);
            return RedirectToAction("Index");

        }
    }
}
