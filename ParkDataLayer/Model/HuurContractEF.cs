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
        [Key]
        [Column(TypeName = "nvarchar(25)")]
        public string Id { get; set; }

        public int HuurPeriodeId { get; set; }
        public HuurPeriodeEF HuurPeriode { get; set; }

        public int HuurderId { get; set; }
        public HuurderEF Huurder { get; set; }

        public int HuisId { get; set; }
        public HuisEF Huis { get; set; }
    }
}
