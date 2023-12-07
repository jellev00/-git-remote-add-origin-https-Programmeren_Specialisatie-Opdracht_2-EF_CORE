using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class HuurContractEF
    {
        public HuurContractEF()
        {
        }

        public HuurContractEF(string id, DateTime startDatum, DateTime eindDatum, int aantalDagen, HuurderEF huurder, HuisEF huis)
        {
            Id = id;
            StartDatum = startDatum;
            EindDatum = eindDatum;
            AantalDagen = aantalDagen;
            Huurder = huurder;
            Huis = huis;
        }

        [Key]
        [Column(TypeName = "nvarchar(25)")]
        public string Id { get; set; }

        [Required]
        public DateTime StartDatum { get; set; }

        [Required]
        public DateTime EindDatum { get; set; }

        [Required]
        public int AantalDagen { get; set; }

        public HuurderEF Huurder { get; set; }

        public HuisEF Huis { get; set; }
    }
}
