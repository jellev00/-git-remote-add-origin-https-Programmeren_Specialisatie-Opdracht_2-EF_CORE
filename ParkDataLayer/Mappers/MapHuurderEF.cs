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
                return new HuurderEF();
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuurderEF - MapToDB", ex);
            }
        }
    }
}
