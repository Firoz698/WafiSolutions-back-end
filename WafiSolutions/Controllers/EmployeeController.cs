using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using WafiSolutions.core;
using WafiSolutions.service.Interface;

namespace WafiSolutions.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeInterface _iemployeeInterface;
        public EmployeeController(IEmployeeInterface iemployeeInterface)
        {
            _iemployeeInterface = iemployeeInterface;
        }


        //AddEmployee api is here
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> AddEmployee()
        {
            Employee Oemployee = new Employee();
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = Request.Form.Files[0];
                var data = JsonConvert.DeserializeObject<Employee>(formCollection["object"]);
                if (file == null)
                {
                    throw new Exception("File Not Found");
                }
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    if (file.Length > 0)
                    {
                        Oemployee.Id = data.Id;
                        Oemployee.FirstName = data.FirstName ;
                        Oemployee.LastName = data.LastName;
                        Oemployee.Image = ms.ToArray();
                        Oemployee.Email = data.Email;
                        Oemployee.Mobile = data.Mobile;
                        Oemployee.DateOfBirth = data.DateOfBirth;
                        var TempObj = _iemployeeInterface.Add(Oemployee);
                    }
                }
                return Ok(Oemployee);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //GetAll api is here
        [HttpPost]
        public async Task<EmployeeResponseDto> GetAllEmployee(EmployeeFilter employeeFilter)
        {
            return await _iemployeeInterface.GetAll(employeeFilter);
        }
        //SingleEmployGetByID api is here
        [HttpGet]
        public Employee GetEmployeeById(int Id)
        {
            return _iemployeeInterface.GetEmployeeById(Id);
        }

        //UpdateEmployee api is here
        [HttpPost , DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateEmployee()
        {
            Employee Oemployee = new Employee();
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = Request.Form.Files[0];
                var data = JsonConvert.DeserializeObject<Employee>(formCollection["object"]);
                if (file == null)
                {
                    throw new Exception("File Not Found");
                }
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    if (file.Length > 0)
                    {
                        Oemployee.Id = data.Id;
                        Oemployee.FirstName = data.FirstName;
                        Oemployee.LastName = data.LastName;
                        Oemployee.Image = ms.ToArray();
                        Oemployee.Email = data.Email;
                        Oemployee.Mobile = data.Mobile;
                        Oemployee.DateOfBirth = data.DateOfBirth;
                        var TempObj = _iemployeeInterface.Update(Oemployee);
                    }
                }
                return Ok(Oemployee);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Delete api is here
        [HttpDelete]
        public Task<Employee> DeletedEmployee(int Id)
        {
            return _iemployeeInterface.DeleteById(Id);
            
        }
    }
}
