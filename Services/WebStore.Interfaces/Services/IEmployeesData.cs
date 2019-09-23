using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        void AddNew(Employee employee);

        Employee UpdateEmployee(int id, Employee employee);

        void Delete(int id);

        void SaveChanges();
    }
}
