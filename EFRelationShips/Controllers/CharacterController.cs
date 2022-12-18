using AutoMapper;
using EFRelationShips.Configurations;
using EFRelationShips.Data;
using EFRelationShips.Dto.Character;
using EFRelationShips.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFRelationShips.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CharacterController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetAllCharacters()
        {
            var characters = await context.Characters.Include(c => c.Weapon).Include(c => c.Skills).ToListAsync();
            return Ok(characters);
        }
        [HttpGet("character{Id}")]
        public async Task<ActionResult<Character>> GetCharacterById(int Id)
        {
            var character = await context.Characters.Where(c => c.Id == Id).Include(c => c.Weapon).Include(c => c.Skills).FirstOrDefaultAsync();
            return Ok(character);
        }
        [HttpGet("user{Id}")]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharactersByUserId(int Id)
        {
            var characters = await context.Characters.Where(character => character.UserId == Id).Include(c => c.Weapon).Include(c => c.Skills).ToListAsync();
            return Ok(characters);
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Character>>> Create(CreateCharacterDto character_dto)
        {
            var user = await context.Users.FindAsync(character_dto.UserId);
            if (user == null)
            {
                return NotFound();
            }
            var character = mapper.Map<Character>(character_dto);
            await context.Characters.AddAsync(character); //check whether user exist
            await context.SaveChangesAsync();
            return await GetCharactersByUserId(character.UserId);
        }
        [HttpPost("weapon")]
        public async Task<ActionResult<Character>> AddWeaponForCharacter(AddCharacterWeaponDto weapon_dto)
        {
            var character = await context.Characters.FindAsync(weapon_dto.CharacterId);
            if (character == null)
            {
                return NotFound();
            }
            var new_weapon = mapper.Map<Weapon>(weapon_dto);
            await context.Weapons.AddAsync(new_weapon); //check whether user exist
            await context.SaveChangesAsync();
            return Ok(character);
        }
        [HttpPost("skill")]
        public async Task<ActionResult<Character>> AddSkillForCharacter(AddCharacterSkillDto skill_dto)
        {
            var character = await context.Characters.Where(c => c.Id == skill_dto.CharacterId).Include(c => c.Skills).FirstOrDefaultAsync();
            if (character == null)
            {
                return NotFound();
            }
            var skill = await context.Skills.FindAsync(skill_dto.SkillId);
            if (skill == null)
            {
                return NotFound();
            }
            character.Skills.Add(skill); //check whether user exist
            await context.SaveChangesAsync();
            return Ok(character);
        }
        [HttpPut]
        public async Task<ActionResult<Character>> UpdateCharacter(UpdateCharacterDto character_dto)
        {
            var character = await context.Characters.Where(c => c.Id == character_dto.CharacterId).Include(c => c.Skills).FirstOrDefaultAsync();
            if (character == null)
            {
                return NotFound();
            }
            character.Name = character_dto.Name;
            character.RPGClass = character_dto.RPGClass;
            await context.SaveChangesAsync();
            return Ok(character);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Character>> DeleteCharacter(int Id)
        {
            var character = await context.Characters.Where(c => c.Id == Id).FirstOrDefaultAsync();
            context.Remove(character);
            await context.SaveChangesAsync();   
            return Ok(character);
        }
    }
}
