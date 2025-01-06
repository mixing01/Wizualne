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
        IProducer IGame.Producer {
            get => Producer;
            set{
                Producer = value as Producer;
                ProducerId = value.Id;
            }
        }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Range(1980, 2025, ErrorMessage = "Invalid release year")]
        public int ReleaseYear { get; set; }
        [Required]
        public GameGenre Genre { get; set; }
        [Required]
        [Range(0, 10000, ErrorMessage = "Invalid price")]
        public double Price { get; set; }
        [Required]
        [Range(0.1, 1000, ErrorMessage = "Invalid disk space")]
        public double DiskSpace { get; set; }
        [Required]
        [Range(0, 10, ErrorMessage = "Invalid rating")]
        public int Rating { get; set; }
    }
}
