using EmpSystemApp.Models;

using EmpSystemApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EmpSystemApp.Services.EmpService;

namespace EmpSystemApp.Controllers
{
    public class InternController : Controller
    {

        private readonly IEmpService _empService;

        public InternController(IEmpService empService)
        {
            _empService = empService;
        }
        public IActionResult InternList()
        {

            AllModel model = new AllModel();

            model.InternList = _empService.getAllEmp();
            return View(model);


        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Employees employee)
        {


            var gg = _empService.Add(employee);
            return RedirectToAction("InternList");

            return View(gg);


        }
        [HttpGet, ActionName("Update")]
        public IActionResult Update(int id)
        {

            var employees = _empService.GetById(id).FirstOrDefault();

            if (employees == null)
            {
                TempData["message"] = "NULL";
                return RedirectToAction(nameof(Index));
            }

            return View(employees);
        }



       

        public IActionResult Update(int id, Employees employees)
        {

            if (id != employees.EmployeeId)
            {
                return NotFound();
            }



            bool up = _empService.Update(employees);


            return RedirectToAction("InternList");
            if (up)
            {
                TempData["SuccessMessage"] = "Updated Correctly";
            }
            else
            {
                TempData["ErrorMessage"] = " Did not Updated Correctly";
            }

            return View();


        }
    }
}
