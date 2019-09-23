using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/Employees")]
    //[Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        [HttpGet, ActionName("Get")]
        public IEnumerable<Employee> GetAll()
        {
            return _EmployeesData.GetAll();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public Employee GetById(int id)
        {
            return _EmployeesData.GetById(id);
        }

        [HttpPost, ActionName("Post")]
        public void AddNew(Employee employee)
        {
            _EmployeesData.AddNew(employee);
        }

        [HttpPut("{id}"), ActionName("Put")]
        public Employee UpdateEmployee(int id, Employee employee)
        {
            return _EmployeesData.UpdateEmployee(id, employee);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _EmployeesData.Delete(id);
        }

        [NonAction]
        public void SaveChanges()
        {
            _EmployeesData.SaveChanges();
        }
    }
}