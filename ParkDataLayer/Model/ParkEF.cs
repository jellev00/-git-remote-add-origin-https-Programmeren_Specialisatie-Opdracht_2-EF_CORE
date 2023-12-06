using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class ParkEF
    {
        [Key]
        [Column(TypeName = "nvarchar(20)")]
        public string Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Naam { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Locatie { get; set; }

        public List<HuisEF> Huizen { get; set; }
    }
}
