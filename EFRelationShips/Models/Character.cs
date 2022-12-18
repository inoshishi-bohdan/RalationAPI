using System.Text.Json.Serialization;

namespace EFRelationShips.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RPGClass { get; set; } = "Wizard";
        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }
        public Weapon Weapon { get; set; } // this if for creation of the foreign keys
        public List<Skill> Skills { get; set; }
    }
}
