using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Podsosus2.Models
{
    public class Cell
    {
        public int Id { get; set; }
        public string? Title { get; set; } // Теперь может быть null
    }


}
