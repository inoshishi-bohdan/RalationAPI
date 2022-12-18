namespace EFRelationShips.Dto.Character
{
    public class CreateCharacterDto
    {
        public string Name { get; set; } = string.Empty;
        public string RPGClass { get; set; } = "Wizard";
        public int UserId { get; set; }

    }
}
