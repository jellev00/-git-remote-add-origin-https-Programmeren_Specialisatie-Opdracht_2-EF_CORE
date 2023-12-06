using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ParkDataLayer.Model
{
    public class HuurderEF
    {
        public HuurderEF()
        {
            // Default constructor Dit is nodig voor EF
        }

        public HuurderEF(int id, string naam, ContactGegevensEF contactGegevens)
        {
            Id = id;
            Naam = naam;
            ContactGegevens = contactGegevens;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Naam { get; set; }

        public int ContactGegevensId { get; set; }
        public ContactGegevensEF ContactGegevens { get; set; }
        public List<HuurContractEF> HuurContracten { get; set; }
    }
}
