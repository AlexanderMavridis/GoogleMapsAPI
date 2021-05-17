using GoogleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Repositories
{
    public interface IEmployeeAttributeRepository
    {
        Task<IEnumerable<EmployeeAttribute>> Get();
    }
}
