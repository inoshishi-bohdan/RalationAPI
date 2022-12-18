namespace EFRelationShips.Dto.Character
{
    public class UpdateCharacterDto
    {
        public string Name { get; set; } = string.Empty;
        public string RPGClass { get; set; } = string.Empty;
        public int CharacterId { get; set; }
    }
}
