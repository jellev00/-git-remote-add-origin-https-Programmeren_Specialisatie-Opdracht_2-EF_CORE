using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using ParkDataLayer.Exceptions;

namespace ParkDataLayer.Mappers
{
    public class MapHuurContractEF
    {
        public static Huurcontract MapToDomain(HuurContractEF db)
        {
            try
            {
                if (db == null || db.StartDatum == null)
                {
                    throw new ArgumentNullException("db or db.StartDatum is null");
                }

                Huurperiode huurperiode = new Huurperiode(db.StartDatum, db.AantalDagen);
                Contactgegevens contactgegevens = new Contactgegevens(db.Huurder.Email, db.Huurder.Telefoon, db.Huurder.Adres);
                Huurder huurder = new Huurder(db.Huurder.Id, db.Huurder.Naam, contactgegevens);

                if (db.Huis != null && db.Huis.Park != null)
                {
                    Park park = new Park(db.Huis.Park.Id, db.Huis.Park.Naam, db.Huis.Park.Locatie);
                    Huis huis = new Huis(db.Huis.Id, db.Huis.Straat, db.Huis.Nummer, db.Huis.Actief, park);

                    return new Huurcontract(db.Id, huurperiode, huurder, huis);
                }
                else
                {
                    throw new ArgumentNullException("db.Huis or db.Huis.Park is null");
                }
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuurContractEF - MapToDomain", ex);
            }
        }

        public static HuurContractEF MapToDB(Huurcontract h, ParkBeheerContext ctx)
        {
            try
            {
                HuisEF huis = ctx.Huis.Find(h.Huis.Id);
                HuurderEF huurder = ctx.Huurder.Find(h.Huurder.Id);

                if (huis == null)
                {
                    huis = MapHuisEF.MapToDB(h.Huis, ctx);
                }
                if (huurder == null)
                {
                    huurder = MapHuurderEF.MapToDB(h.Huurder, ctx);
                }

                if (huis != null)
                {
                    huis.Straat = h.Huis.Straat;
                    huis.Nummer = h.Huis.Nr;
                    huis.Park = MapParkEF.MapToDB(h.Huis.Park, ctx);
                }

                return new HuurContractEF(h.Id, h.Huurperiode.StartDatum, h.Huurperiode.EindDatum, h.Huurperiode.Aantaldagen, huurder, huis);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuurContractEF - MapToDB", ex);
            }
        }
    }
}
