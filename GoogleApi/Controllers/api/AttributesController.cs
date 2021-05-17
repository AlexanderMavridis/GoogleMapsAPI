using AutoMapper;
using GoogleApi.Dtos;
using GoogleApi.Helpers;
using GoogleApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;

        public AttributesController(IAttributeRepository attributeRepository, IMapper mapper)
        {
            _attributeRepository = attributeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AttributeDto>> GetAttributes()
        {
            var attributes = _attributeRepository.Get();
            var attributesDto = new List<AttributeDto>();

            foreach (var attribute in await attributes)
                //attributesDto.Add(MyMapper.MapToAttributeDto(attribute));
                attributesDto.Add(_mapper.Map<AttributeDto>(attribute));
                

            return attributesDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeDto>> GetAttribute(Guid id)
        {
            var attribute = await _attributeRepository.Get(id);
            if (attribute == null)
                return NotFound();

            //var attributeDto = MyMapper.MapToAttributeDto(attribute);
            var attributeDto = _mapper.Map<AttributeDto>(attribute);
            
            return attributeDto;
        }

        [HttpPut("updateAttribute/{id}")]
        public async Task<ActionResult> PutAttribute(Guid id, [FromBody] AttributeDto attributeDto)
        {
            if (id != attributeDto.AttrId)
                return BadRequest();

            //var attribute = MyMapper.MapToAttribute(attributeDto);
            var attribute = _mapper.Map<Models.Attribute>(attributeDto);
            await _attributeRepository.Update(attribute);

            return NoContent();
        }

        [HttpPost("newAttribute")]
        public async Task<ActionResult<AttributeDto>> PostAttribute([FromBody] AttributeDto attributeDto)   //  should i exclude Guid from model binder?  8a bgalei error h modelstate?
        {                                                                                                   //  ti 8a ginotan stin put ??
            if (!ModelState.IsValid)
                return BadRequest();

            //var attribute = MyMapper.MapToAttribute(attributeDto);
            var attribute = _mapper.Map<Models.Attribute>(attributeDto);
            await _attributeRepository.Create(attribute);

            return CreatedAtAction("GetAttribute", new { id = attributeDto.AttrId }, attributeDto);
        }

        [HttpDelete("deleteAttribute/{id}")]
        public async Task<ActionResult> DeleteAttribute(Guid id)
        {
            var attributeToDelete = await _attributeRepository.Get(id);
            if (attributeToDelete == null)
                return NotFound();

            await _attributeRepository.Delete(attributeToDelete.AttrId);   // the await here destroyed my night... WHY?? eftege o postman? ap to swagger den bgazei thema
            return NoContent();
        }

        //[HttpGet("groupedAttributes")]
        //public async Task<IEnumerable<Group<string, Models.Attribute>>> GetGroups()     // another time
        //{
        //    var groupedAttributes =  _attributeRepository.GetGrouped();

        //    return await groupedAttributes;
        //}
    }
}
