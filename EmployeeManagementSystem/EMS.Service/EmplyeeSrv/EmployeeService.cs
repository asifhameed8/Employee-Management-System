using AutoMapper;
using EMS.Core.Context;
using EMS.Core.Repositories;
using EMS.Models.Command;
using EMS.Models.Entities;
using EMS.Models.ViewModels;
using EMS.Service.Uow;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.EmplyeeSrv
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IGenericRepository<Employee> GetRepository()
        {
            return _unitOfWork.GetRepository<Employee>();
        }
        public Tuple<IEnumerable<EmployeeVM>, long> GetAllEmployee(EmployeeCommand parm)
        {
            Expression<Func<Employee, bool>> predicate = x => (parm.Name != null ? x.Name.ToLower().Contains(parm.Name.ToLower()) : true)
            && (parm.Email != null ? x.Email.ToLower().Contains(parm.Email.ToLower()) : true)
            && (parm.DepartmentName != null ? x.DepartmentName == parm.DepartmentName : true);


            Expression<Func<Employee, object>> OrderBy = null;
            Expression<Func<Employee, object>> OrderByDesc = x => x.Id;
            var employeeList = GetRepository().GetPagingWhereAsync(predicate, parm.PagingData, OrderBy, OrderByDesc, null, null);
            if (employeeList != null)
            {
                var emplyees = _mapper.Map<IEnumerable<EmployeeVM>>(employeeList.Item1);
                return new Tuple<IEnumerable<EmployeeVM>, long>(emplyees, employeeList.Item2);
            }
            else
            {
                return null;
            }
        }
        public Employee GetEmployeeById(long id)
        {
            Expression<Func<Employee, bool>> predicate = x => x.Id == id;
            return GetRepository().GetFirstAsyncNoTrack(predicate, null).Result;
        }
        public long SaveChanges()
        {
            return _unitOfWork.Commit();
        }

        public async Task<long> SaveEmployee(Employee model)
        {

            if (model.Id != 0)
                GetRepository().Update(model);
            else
                GetRepository().Add(model);

            SaveChanges();
            return model.Id;
        }

        public string DeleteEmployee(long id)
        {
            var employee = GetRepository().GetFirstAsyncNoTrack(o => o.Id == id).Result;
            if (employee != null)
            {
                GetRepository().HardDelete(employee);
                return "Success";
            }
            else
            {
                return "Records not found";
            }
        }
    }
}
