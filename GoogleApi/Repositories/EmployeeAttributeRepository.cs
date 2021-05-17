using GoogleApi.Data;
using GoogleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Repositories
{
    public class EmployeeAttributeRepository : IEmployeeAttributeRepository
    {
        private readonly GoogleApiProjectDBContext _context;

        public EmployeeAttributeRepository(GoogleApiProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeAttribute>> Get()
        {
            return  _context.EmployeeAttributes.ToList();
        }
    }
}
