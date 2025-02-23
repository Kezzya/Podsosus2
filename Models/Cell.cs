using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Podsosus2.Models
{
    public class Cell
    {

        public int id { get; set; } 
        public string Title { get; set; } //e2
        public double X { get; set; } //
        public double Y { get; set; } // 
        public double Z { get; set; }
    }
        
    
}
