using AutoMapper;
using EFRelationShips.Dto.Character;
using EFRelationShips.Dto.Skill;
using EFRelationShips.Dto.User;
using EFRelationShips.Dto.Weapon;
using EFRelationShips.Migrations;
using EFRelationShips.Models;

namespace EFRelationShips.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CreateCharacterDto, Character>();
            CreateMap<AddCharacterWeaponDto, Weapon>();
            CreateMap<User, ReadUserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>();
            CreateMap<Weapon, ReadWeaponDto>();
            CreateMap<CreateWeaponDto, Weapon>();

            CreateMap<Skill, ReadSkillDto>();
            CreateMap<CreateSkillDto, Skill>();
        }
    }
}
