using Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAOEF.BO
{
    public class Game : IGame
    {
        public int Id { get; set; }
        
        public Producer Producer { get; set; }
        public int ProducerId { get; set; } 

        [NotMapped]
        [Required]
        IProducer IGame.Producer {
            get => Producer;
            set{
                Producer = value as Producer;
                ProducerId = value.Id;
            }
        }
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [Required]
        [Range(1900, 2025, ErrorMessage = "Invalid release year (1900-2025)")]
        public int ReleaseYear { get; set; }
        [Required]
        public GameGenre Genre { get; set; }
        [Required]
        [Range(0.0, 10000.0, ErrorMessage = "Invalid price (0-10000)")]
        public double Price { get; set; }
        [Required]
        [Range(0.1, 1000.0, ErrorMessage = "Invalid disk space (0.1-1000)")]
        public double DiskSpace { get; set; }
        [Required]
        [Range(0, 10, ErrorMessage = "Invalid rating (0-10)")]
        public int Rating { get; set; }
    }
}
