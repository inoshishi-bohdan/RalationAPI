namespace EFRelationShips.Dto.Character
{
    public class AddCharacterWeaponDto
    {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public int CharacterId { get; set; }
    }
}
