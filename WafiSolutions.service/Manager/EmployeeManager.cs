using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafiSolutions.core;
using WafiSolutions.infracturcture;
using WafiSolutions.service.Interface;

namespace WafiSolutions.service.Manager
{
    public class EmployeeManager : IEmployeeInterface
    {
        public DBContext _DataBase;
        public EmployeeManager(DBContext dataBase)
        {
            _DataBase = dataBase;
        }

        public async Task<IActionResult> Add(Employee employee)
        {
            try
            {
                await _DataBase.Employees.AddAsync(employee);
                _DataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        public async Task<EmployeeResponseDto> GetAll(EmployeeFilter employeeFilter)
        {
            try
            {
                if (employeeFilter.PageNo <= 0 || employeeFilter.PageSize <= 0)
                {
                    throw new Exception("Invalid page number or page size.");
                }

                var query = _DataBase.Employees.AsQueryable();
                var totalItems = await query.CountAsync(); // Await CountAsync
                var pagedItems = await query
                    .Skip((employeeFilter.PageNo - 1) * employeeFilter.PageSize)
                    .Take(employeeFilter.PageSize)
                    .ToListAsync(); // Await ToListAsync
                var response = new EmployeeResponseDto
                {
                    TotalItems = totalItems,
                    PageNo = employeeFilter.PageNo,
                    PageSize = employeeFilter.PageSize,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)employeeFilter.PageSize),
                    Items = pagedItems ?? new List<Employee>()
                };
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Employee GetEmployeeById(int Id)
        {
            return _DataBase.Employees.FirstOrDefault(x=>x.Id==Id);
            
        }
        public async Task<IActionResult> Update(Employee employee)
        {
            Employee StorUser = new Employee();
            try
            {
               _DataBase.Employees.Update(employee);
               _DataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }
        public async Task<Employee> DeleteById(int Id)
        {
            try
            {
                var employee = await _DataBase.Employees.FindAsync(Id);
                if (employee != null)
                {
                    _DataBase.Employees.Remove(employee);
                    _DataBase.SaveChanges();
                }
                return employee;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
