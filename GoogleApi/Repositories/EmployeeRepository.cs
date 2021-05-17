using GoogleApi.Data;
using GoogleApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly GoogleApiProjectDBContext _context;

        public EmployeeRepository(GoogleApiProjectDBContext context)
        {
            _context = context;
        }

        public async Task<Employee> Create(Employee employee)
        {
            employee.EmpId = Guid.NewGuid();
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task Delete(Guid id)
        {
            var employeeToDelete = await _context.Employees.FindAsync(id);           
            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            return await _context.Employees.
                Include(e => e.EmployeeAttributes).
                ToListAsync();
        }

        public async Task<Employee> Get(Guid id)
        {
            return await _context.Employees.Include(e => e.EmployeeAttributes).FirstOrDefaultAsync(e => e.EmpId == id);
        }

        public async Task Update(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeesAttributes(Employee employee)
        {
            var newEmployee = await _context.Employees
                .Include(e => e.EmployeeAttributes)
                .FirstOrDefaultAsync(e => e.EmpId == employee.EmpId);

            newEmployee.EmployeeAttributes.Clear();

            var availableAttributes =  _context.Attributes.ToList();
            foreach (var record in employee.EmployeeAttributes)
            {
                var attribute = availableAttributes.FirstOrDefault(a => a.AttrId == record.EmpattrAttributeId);
                
                if (attribute != null)
                {
                    var id = attribute.AttrId;
                    newEmployee.EmployeeAttributes.Add(new EmployeeAttribute { EmpattrAttributeId = id, EmpattrEmployeeId = employee.EmpId });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task PostEmployeesAttributes(Employee employee)
        {
            var newEmployee = await _context.Employees                     // idia diakasia me panw giati ginontai diadoxika ajax(ena gia ton employee kai ena gia ta attributes) 
                .Include(e => e.EmployeeAttributes)
                .FirstOrDefaultAsync(e => e.EmpId == employee.EmpId);

            foreach (var record in employee.EmployeeAttributes)
                newEmployee.EmployeeAttributes.Add(new EmployeeAttribute { EmpattrAttributeId = record.EmpattrAttributeId, EmpattrEmployeeId = employee.EmpId });

            await _context.SaveChangesAsync();
        }

        public bool validEmployeeAttributes(Employee employee)
        {
            var employeeAttributesIdsList = employee.EmployeeAttributes.Select(ea => new { id = ea.EmpattrAttributeId }).ToList();
            var attributesList = _context.Attributes.ToList();
            var FinalList = new List<string>();

            foreach (var row in employeeAttributesIdsList)
                FinalList.Add(attributesList.FirstOrDefault(a => a.AttrId == row.id).AttrName);

            bool hasDuplicates = FinalList.GroupBy(d => d).Any(g => g.Count() > 1);
            return hasDuplicates;
        }

        public async Task<IEnumerable<Employee>> GetRelatedData(Guid attributeId)
        {
            var query = from employee in _context.Employees
                        where employee.EmployeeAttributes.Any(ea => ea.EmpattrAttributeId == attributeId)
                        select employee;

            return await query.ToListAsync();
        }
    }
}
