using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFRelationShips.Data;
using EFRelationShips.Models;
using AutoMapper;
using EFRelationShips.Dto.Skill;
using EFRelationShips.Dto.Weapon;

namespace EFRelationShips.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper mapper;

        public SkillsController(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return await _context.Skills.ToListAsync();
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadSkillDto>> GetSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            return mapper.Map<ReadSkillDto>(skill);
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(int id, UpdateSkillDto skill_dto)
        {
            var skill = await _context.Skills.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (skill == null)
            {
                return NotFound();
            }
            skill.Name = skill_dto.Name;
            skill.Damage = skill_dto.Damage;
            await _context.SaveChangesAsync();
            return Ok(mapper.Map<ReadSkillDto>(skill));
        }

        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill(CreateSkillDto skill_dto)
        {
            var skill = mapper.Map<Skill>(skill_dto);
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkill", new { id = skill.Id }, skill);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return Ok(skill);
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
