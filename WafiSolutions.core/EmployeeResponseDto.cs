using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WafiSolutions.core
{
    public class EmployeeResponseDto
    {
        public EmployeeResponseDto()
        {
            TotalItems = 0;
            PageNo = 0;
            PageSize = 0;
            TotalPages = 0;
            Items = new List<Employee>();
        }
        public int TotalItems { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<Employee> Items { get; set; }
    }
}
