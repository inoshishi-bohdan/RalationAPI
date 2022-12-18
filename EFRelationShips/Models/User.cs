using Newtonsoft.Json;

namespace EFRelationShips.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Character> Characters { get; set; }
    }
}
