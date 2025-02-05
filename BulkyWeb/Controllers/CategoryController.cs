using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
		{
			//._db.DbSets
			List<Category> categories = _db.Categories.ToList();
			return View(categories);
		}
		public IActionResult Create()
		{
			return View();
			//return View("~/Views/Category/Create.cshtml");
		}

		[HttpPost]
		public IActionResult Create(Category obj)
		{
			/*
			//custom validation
			if (obj.Name == obj.DisplayOrder.ToString()) {
				ModelState.AddModelError("Name", "name must not similat to the display order value .");
				//ModelState.AddModelError(key [validation on any prperty], error message);
			}

			if (obj.Name.ToLower() == "test")
			{
				ModelState.AddModelError("", "test is an invalid value.");
				//dont show under the name property  but shown in the validation summary part .
			}
			*/

			if (ModelState.IsValid) //if obje valid accroding to data annotation validations 
			{
				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "Category Added successfully .";

				return RedirectToAction("Index");
			}
			//RedirectToAction : causes Reload .

			return View();
		}


		public IActionResult Edit(int? categoryId)
		{

			if (categoryId == null || categoryId==0) {
				return NotFound();
			}
			

			Category? SelectedCategory = _db.Categories.Find(categoryId); //find works for primary keys
		//	Category? SelectedCategory2 = _db.Categories.FirstOrDefault(u=>u.Id==categoryId); //find works for primary keys and not and open to validations : .FirstOrDefault(u=>u.Name.contains(""));
		//	Category? SelectedCategory3 = _db.Categories.Where(u => u.Id == categoryId).FirstOrDefault();
			if (SelectedCategory == null) { 
				return NotFound();
			}
			return View(SelectedCategory);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid) 
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category updated successfully .";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? categoryId)
		{
			if (categoryId == null || categoryId == 0)
			{
				return NotFound();
			}
			Category? SelectedCategory = _db.Categories.Find(categoryId); 
								
			if (SelectedCategory == null)
			{
				return NotFound();
			}
			return View(SelectedCategory);
		}

		[HttpPost , ActionName("Delete")]
		//ActionName("Delete") : solve error of [same name and same parameter] .
		public IActionResult DeletePost(int? categoryId)
		{
			Category? Selectedcategory = _db.Categories.Find(categoryId);
			if (Selectedcategory == null) { return NotFound(); }
			_db.Categories.Remove(Selectedcategory);
			_db.SaveChanges();
			TempData["success"] = "Category Deleted successfully .";

			return RedirectToAction("Index");
		}
	}
}
