using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models.Command
{
    public class EmployeeCommand
    {
        public EmployeeCommand()
        {
            PagingData = new PagingData(null,null);
        }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? DepartmentName { get; set; }
        public PagingData PagingData { get; set; }
    }
}
