namespace Podsosus2.Models
{
    public class Player
    {
        public int Id { get; set; }
        // Список ссылок на платформы
        public List<string> Links { get; set; } = new List<string>();
        public required string Nickname { get; set; }
        public required string position { get; set; } //e2
    }
}
