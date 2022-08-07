using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project278.Models
{
    public class ArtistBand
    {
        [Key, Column(Order = 1)]
        public int ArtistId { get; set; }
        [Key, Column(Order = 2)]
        public int BandId { get; set; }
        public Artist Artist { get; set; }
        public Band Band { get; set; }
    }
}
