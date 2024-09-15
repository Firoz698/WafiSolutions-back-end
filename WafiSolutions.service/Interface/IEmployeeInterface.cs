using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafiSolutions.core;

namespace WafiSolutions.service.Interface
{
    public interface IEmployeeInterface
    {
        Task<EmployeeResponseDto> GetAll(EmployeeFilter employeeFilter);
        Employee GetEmployeeById(int Id);
        Task<IActionResult> Add(Employee product);
        Task<IActionResult> Update(Employee product);
        Task<Employee> DeleteById(int Id);

    }
}
