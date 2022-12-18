using System.Text.Json.Serialization;

namespace EFRelationShips.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; } = 20;
        [JsonIgnore]
        public List<Character> Characters { get; set; }
    }
}
