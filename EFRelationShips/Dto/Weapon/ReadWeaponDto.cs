﻿namespace EFRelationShips.Dto.Weapon
{
    public class ReadWeaponDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; } = 10;
        public int CharacterId { get; set; }
    }
}
