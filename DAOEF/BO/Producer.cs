using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Interfaces;

namespace DAOEF.BO
{
    public class Producer : IProducer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [NotMapped]
        public List<Game> Games { get; set; }
        [Required]
        [Range(1800,2025,ErrorMessage = "Invalid established year")]
        public int EstYear { get; set; }
        [Required]
        public Continent Continent { get; set; }
    }
}