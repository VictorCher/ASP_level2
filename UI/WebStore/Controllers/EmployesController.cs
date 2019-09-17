using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("Users/[action]")]
    [Authorize]
    public class EmployesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        public IActionResult Index() => View(_EmployeesData.GetAll());

        //[Route("Info/{id}")]
        //[ActionName("EmployesDetails")]
        public IActionResult Details(int id)
        {
            var employe = _EmployeesData.GetById(id);
            return employe == null 
                ? (IActionResult) NotFound() 
                : View("EmployeeView", employe);
        }

        [Authorize(Roles = Domain.Entities.User.RoleAdmin)]
        public IActionResult Edit(int? id)
        {
            Employee employee;
            if (id != null)
            {
                employee = _EmployeesData.GetById((int)id);
                if (employee is null)
                    return NotFound();
            }
            else
            {
                employee = new Employee();
            }

            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = Domain.Entities.User.RoleAdmin)]
        public IActionResult Edit(Employee employee, [FromServices] IMapper mapper)
        {
            if(employee.Age < 18)
                ModelState.AddModelError("Age", "Возраст слишком маленький");

            if (employee.Age > 120)
                ModelState.AddModelError("Age", "Странный возраст");

            //if(employee.Age % 2 == 0)
            //    ModelState.AddModelError("", "Чётный возраст");

            if (!ModelState.IsValid) return View(employee);

            if (employee.Id > 0)
            {
                var db_employee = _EmployeesData.GetById(employee.Id);
                if (db_employee is null)
                    return NotFound();

                //AutoMapper.Mapper.Map(employee, db_employee);
                mapper.Map(employee, db_employee);

                //db_employee.FirstName = employee.FirstName;
                //db_employee.SurName = employee.SurName;
                //db_employee.Patronymic = employee.Patronymic;
                //db_employee.Age = employee.Age;
            }
            else
                _EmployeesData.AddNew(employee);

            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Domain.Entities.User.RoleAdmin)]
        public IActionResult Delete(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null) return NotFound();
            _EmployeesData.Delete(id);
            return RedirectToAction("Index");
        }
    }
}