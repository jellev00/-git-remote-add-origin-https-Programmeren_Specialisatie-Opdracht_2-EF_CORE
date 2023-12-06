using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class ContactGegevensEF
    {
        public ContactGegevensEF()
        {
            // Default constructor Dit is nodig voor EF
        }

        public ContactGegevensEF(string telefoon, string email, string adres)
        {
            Telefoon = telefoon;
            Email = email;
            Adres = adres;
        }

        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Telefoon { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Adres { get; set; }

        public HuurderEF Huurder { get; set; }
    }
}
