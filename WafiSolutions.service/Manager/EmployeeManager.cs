using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WafiSolutions.core;
using WafiSolutions.infracturcture;
using WafiSolutions.service.Interface;

namespace WafiSolutions.service.Manager
{
    public class EmployeeManager : IEmployeeInterface
    {
        public DBContext _dataBase;
        public EmployeeManager(DBContext dataBase)
        {
            _dataBase = dataBase;
        }

        public async Task<IActionResult> Add(Employee employee)
        {
            try
            {
                await _dataBase.Employees.AddAsync(employee);
                _dataBase.SaveChanges();
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

                var query = _dataBase.Employees.AsQueryable();
                if (!string.IsNullOrEmpty(employeeFilter.Name))
                {
                    query = query.Where(i => i.FirstName.ToLower().Contains(employeeFilter.Name.Trim().ToLower()));
                }

                if (!string.IsNullOrEmpty(employeeFilter.Email))
                {
                    query = query.Where(i => i.Email.ToLower().Contains(employeeFilter.Email.Trim().ToLower()));
                }

                if (!string.IsNullOrEmpty(employeeFilter.Mobile))
                {
                    query = query.Where(i => i.Mobile.Contains(employeeFilter.Mobile.Trim())); 
                }
                //if (!string.IsNullOrEmpty(employeeFilter.DateOfBirth))
                //{
                //    if (DateTime.TryParse(employeeFilter.DateOfBirth, out DateTime parsedDate))
                //    {
                //        query = query.Where(i => i.DateOfBirth.Date == parsedDate.Date);
                //    }
                //}

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

                throw new Exception(ex.Message);
            }
        }

        public Employee GetEmployeeById(int Id)
        {
            return _dataBase.Employees.FirstOrDefault(x=>x.Id==Id);
            
        }
        public async Task<IActionResult> Update(Employee employee)
        {
            Employee StorUser = new Employee();
            try
            {
               _dataBase.Employees.Update(employee);
               _dataBase.SaveChanges();
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
                var employee = await _dataBase.Employees.FindAsync(Id);
                if (employee != null)
                {
                    _dataBase.Employees.Remove(employee);
                    _dataBase.SaveChanges();
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
