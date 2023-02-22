using EMS.Core.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.UnitTest
{
    public class PostUnitTestController
    {
        public static DbContextOptions<DatabaseContext> dbContextOptions { get; }

        static PostUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("EmployeeDB")
                .Options;
        }
    }
}
