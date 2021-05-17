using GoogleApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Repositories
{
    public interface IAttributeRepository
    {
        Task<IEnumerable<Models.Attribute>> Get();
        Task<Models.Attribute> Get(Guid id);
        Task<Models.Attribute> Create(Models.Attribute attribute);
        Task Update(Models.Attribute attribute);
        Task Delete(Guid id);
        Task<IEnumerable<Group<string, Models.Attribute>>>GetGrouped();
    }
}
