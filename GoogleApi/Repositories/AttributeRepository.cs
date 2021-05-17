using GoogleApi.Data;
using GoogleApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Repositories
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly GoogleApiProjectDBContext _context;

        public AttributeRepository(GoogleApiProjectDBContext context)
        {
            _context = context;
        }

        public async Task<Models.Attribute> Create(Models.Attribute attribute)
        {
            attribute.AttrId = Guid.NewGuid();
           _context.Attributes.Add(attribute);
            await _context.SaveChangesAsync();

            return attribute;
        }

        public async Task Delete(Guid id)
        {
            var attributeToDelete = await _context.Attributes.FindAsync(id);
            _context.Attributes.Remove(attributeToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Models.Attribute>> Get()
        {
            return await _context.Attributes.ToListAsync();
        }

        public async Task<Models.Attribute> Get(Guid id)
        {
            return await _context.Attributes.FindAsync(id);
        }

        public async Task Update(Models.Attribute attribute)
        {
            _context.Entry(attribute).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group<string, Models.Attribute>>> GetGrouped()
        {
            var attributesGrouped = from attr in _context.Attributes
                                    group attr by attr.AttrName into g
                                    select new Group<string, Models.Attribute> { Key = g.Key, Values = g };

            return attributesGrouped.ToList();
        }      
    }
}
