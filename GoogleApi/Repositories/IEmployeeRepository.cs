using GoogleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Get();
        Task<Employee> Get(Guid id);
        Task<Employee> Create(Employee employee);
        Task Update(Employee employee);
        Task Delete(Guid id);
        Task UpdateEmployeesAttributes(Employee employee);
        Task PostEmployeesAttributes(Employee employee);
        bool validEmployeeAttributes(Employee employee);
        Task<IEnumerable<Employee>> GetRelatedData(Guid attributeId);
    }
}
