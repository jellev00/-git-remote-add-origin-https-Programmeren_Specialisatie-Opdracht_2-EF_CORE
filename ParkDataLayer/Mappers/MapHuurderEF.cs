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
                return new Huurder(db.Id, db.Naam, new Contactgegevens(db.ContactGegevens.Email, db.ContactGegevens.Telefoon, db.ContactGegevens.Adres));
            } 
            catch (Exception ex)
            {
                throw new MapperException("MapHuurderEF - MapToDomain", ex);
            }
        }

        public static HuurderEF MapToDB(Huurder h)
        {
            try
            {
                return new HuurderEF(h.Id, h.Naam, new ContactGegevensEF(h.Contactgegevens.Email, h.Contactgegevens.Tel, h.Contactgegevens.Adres));
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuurderEF - MapToDB", ex);
            }
        }
    }
}
