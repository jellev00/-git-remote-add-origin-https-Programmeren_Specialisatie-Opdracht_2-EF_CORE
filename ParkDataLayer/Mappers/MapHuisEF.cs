using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers
{
    public class MapHuisEF
    {
        public static Huis MapToDomain(HuisEF db)
        {
            try
            {
                Dictionary<Huurder, List<Huurcontract>> _huurContracten = new Dictionary<Huurder, List<Huurcontract>>();
                List<Huurcontract> huurcontracts = new List<Huurcontract>();

                if (db == null || db.Park == null)
                {
                    throw new ArgumentNullException("db or db.Park is null");
                }

                Park park = new Park(db.Park.Id, db.Park.Naam, db.Park.Locatie);

                foreach (HuurContractEF hc in db.HuurContracten)
                {
                    Contactgegevens contactgegevens = new Contactgegevens(hc.Huurder.Email, hc.Huurder.Telefoon, hc.Huurder.Adres);

                    Huurder huurder = new Huurder(hc.Huurder.Id, hc.Huurder.Naam, contactgegevens);

                    Huurperiode huurperiode = new Huurperiode(hc.StartDatum, hc.AantalDagen);

                    Huis huis = new Huis(hc.Huis.Id, hc.Huis.Straat, hc.Huis.Nummer, hc.Huis.Actief, park);

                    Huurcontract huurcontract = new Huurcontract(hc.Id, huurperiode, huurder, huis);

                    huurcontracts.Add(huurcontract);

                    _huurContracten.Add(huurder, huurcontracts);
                }

                return new Huis(db.Id, db.Straat, db.Nummer, db.Actief, park, _huurContracten);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuisEF - MapToDomain", ex);
            }
        }

        public static HuisEF MapToDB(Huis h, ParkBeheerContext ctx)
        {
            try
            {
                ParkEF park = ctx.Park.Find(h.Park.Id);

                if (park == null)
                {
                    park = MapParkEF.MapToDB(h.Park, ctx);
                }

                List<HuurContractEF> huurcontractes = new List<HuurContractEF>();

                foreach (Huurcontract huurcontract in h.Huurcontracten().ToList())
                {
                    HuurContractEF huurContractEF = MapHuurContractEF.MapToDB(huurcontract, ctx);
                    huurcontractes.Add(huurContractEF);
                }

                HuisEF huis;

                if (h.Id > 0)
                {
                    huis = new HuisEF(h.Id, h.Straat, h.Nr, h.Actief, park, huurcontractes);
                } else
                {
                    huis = new HuisEF(h.Straat, h.Nr, h.Actief, park, huurcontractes);
                }

                return huis;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuisEF - MapToDB", ex);
            }
        }
    }
}
