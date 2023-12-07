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

        public HuurderEF(int id, string naam, string telefoon, string email, string adres, List<HuurContractEF> huurContracten)
        {
            Id = id;
            Naam = naam;
            Telefoon = telefoon;
            Email = email;
            Adres = adres;
            HuurContracten = huurContracten;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Naam { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Telefoon { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Adres { get; set; }

        public List<HuurContractEF> HuurContracten { get; set; }
    }
}
