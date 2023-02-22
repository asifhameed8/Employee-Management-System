using EMS.Core.Repositories;
using EMS.Models.Command;
using EMS.Models.Entities;
using EMS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.EmplyeeSrv
{
    public interface IEmployeeService
    {
        IGenericRepository<Employee> GetRepository();
        Task<long> SaveEmployee(Employee model);
        long SaveChanges();
        Employee GetEmployeeById(long id);
        string DeleteEmployee(long id);
        Tuple<IEnumerable<EmployeeVM>, long> GetAllEmployee(EmployeeCommand parm);
    }
}
