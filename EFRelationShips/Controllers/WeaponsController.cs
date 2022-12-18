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
using EFRelationShips.Dto.Weapon;
using EFRelationShips.Dto.User;

namespace EFRelationShips.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper mapper;

        public WeaponsController(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Weapons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weapon>>> GetWeapons()
        {
            return await _context.Weapons.ToListAsync();
        }

        // GET: api/Weapons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadWeaponDto>> GetWeapon(int id)
        {
            var weapon = await _context.Weapons.FindAsync(id);

            if (weapon == null)
            {
                return NotFound();
            }

            return mapper.Map<ReadWeaponDto>(weapon);
        }

        // PUT: api/Weapons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeapon(int id, UpdateWeaponDto weapon_dto)
        {
            var weapon = await _context.Weapons.Where(w => w.Id == id).FirstOrDefaultAsync();
            if (weapon == null)
            {
                return NotFound();
            }
            weapon.Name = weapon_dto.Name;
            weapon.Damage= weapon_dto.Damage;
            await _context.SaveChangesAsync();
            return Ok(mapper.Map<ReadWeaponDto>(weapon));
        }

        // POST: api/Weapons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Weapon>> PostWeapon(CreateWeaponDto weapon_dto)
        {
            if (await _context.Weapons.Where(w => w.CharacterId == weapon_dto.CharacterId).FirstOrDefaultAsync() != null)
            {
                return BadRequest();
            }
            var weapon = mapper.Map<Weapon>(weapon_dto);
            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetWeapon", new { id = weapon.Id }, weapon);
        }

        // DELETE: api/Weapons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeapon(int id)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }

            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();

            return Ok(weapon);
        }

        private bool WeaponExists(int id)
        {
            return _context.Weapons.Any(e => e.Id == id);
        }
    }
}
