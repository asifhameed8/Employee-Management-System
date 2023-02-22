using EMS.Core.Context;
using EMS.Models.Entities;

namespace EMS.UnitTest
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(DatabaseContext context)
        {
            context.Database.EnsureDeleted();

            context.Employees.AddRange(
                new Employee() { Name = "John Doe", Email = "john@gmail.com", DateOfBirth= Convert.ToDateTime("2/12/1987"), Id = 1, DepartmentName= "Account" },
                new Employee() { Name = "Welliam San", Email = "welliam@gmail.com", DateOfBirth= Convert.ToDateTime("8/6/2007"), Id = 2, DepartmentName= "Account" },
                new Employee() { Name = "Tom", Email = "john@gmail.com", DateOfBirth= Convert.ToDateTime("5/2/1999"), Id = 3, DepartmentName= "Dev" },
                new Employee() { Name = "Hipher", Email = "john@gmail.com", DateOfBirth= Convert.ToDateTime("1/1/2000"), Id = 4, DepartmentName= "IT" }
            );
            context.SaveChanges();
        }
    }
}