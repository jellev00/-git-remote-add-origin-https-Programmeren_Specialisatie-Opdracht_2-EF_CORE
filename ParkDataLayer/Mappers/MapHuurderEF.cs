using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using ParkDataLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers
{
    public class MapHuurderEF
    {
        public static Huurder MapToDomain(HuurderEF db)
        {
            try
            {
                Contactgegevens contactgegevens = new Contactgegevens(db.Email, db.Telefoon, db.Adres);

                return new Huurder(db.Id, db.Naam, contactgegevens);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuurderEF - MapToDomain", ex);
            }
        }

        public static HuurderEF MapToDB(Huurder h, ParkBeheerContext ctx)
        {
            try
            {
                HuurderEF huurder = ctx.Huurder.Find(h.Id);

                if (huurder != null)
                {
                    // Update the existing Park
                    huurder.Naam = h.Naam;
                    huurder.Email = h.Contactgegevens.Email;
                    huurder.Telefoon = h.Contactgegevens.Tel;
                    huurder.Adres = h.Contactgegevens.Adres;
                }
                else
                {
                    // Create a new Park if it doesn't exist
                    huurder = new HuurderEF(h.Id, h.Naam, h.Contactgegevens.Tel, h.Contactgegevens.Email, h.Contactgegevens.Adres);
                    ctx.Huurder.Add(huurder);
                }


                return new HuurderEF(h.Naam, h.Contactgegevens.Tel, h.Contactgegevens.Email, h.Contactgegevens.Adres);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuurderEF - MapToDB", ex);
            }
        }
    }
}
