using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WafiSolutions.core;

public class EmployeeFilter
{
    public EmployeeFilter()
    {
        PageNo = 0;
        PageSize = 0;
        Name = "";
        Email = "";
        Mobile = "";
        DateOfBirth = "";
    }

    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string DateOfBirth { get; set; }
}
