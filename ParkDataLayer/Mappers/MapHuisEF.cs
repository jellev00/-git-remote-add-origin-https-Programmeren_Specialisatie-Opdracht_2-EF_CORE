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
                //ParkEF park = ctx.Park.Find(h.Park.Id);
                //var huurcontracten = h.Huurcontracten().ToList();
                //HuurderEF huurder = null;

                //foreach (Huurcontract hc in huurcontracten)
                //{
                //    huurder = ctx.Huurder.Find(hc.Huurder.Id);
                //}

                //List<HuurContractEF> huurContracten = new List<HuurContractEF>();

                //foreach (Huurcontract hc in h.Huurcontracten())
                //{
                //    HuisEF huis = new HuisEF(hc.Huis.Id, hc.Huis.Straat, hc.Huis.Nr, hc.Huis.Actief, park, );

                //    HuurContractEF huurContractEF = new HuurContractEF(hc.Id, hc.Huurperiode.StartDatum, hc.Huurperiode.EindDatum, hc.Huurperiode.Aantaldagen, huurder, h);
                //    huurContracten.Add(huurContractEF);
                //}


                ////HuurContractEF huurcontract = ctx.HuurContract.Find(h.Huurcontracten)

                //if (huis == null)
                //{
                //    huurder = MapHuurderEF.MapToDB(h.Huurder);
                //}

                //return new HuisEF(h.Id, h.Straat, h.Nr, h.Actief, park, huurContracten);
                return new HuisEF();
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuisEF - MapToDB", ex);
            }
        }
    }
}
