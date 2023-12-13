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
    public class MapParkEF
    {
        public static Park MapToDomain(ParkEF db)
        {
            try
            {

                return new Park(db.Id, db.Naam, db.Locatie);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuurderEF - MapToDomain", ex);
            }
        }

        public static ParkEF MapToDB(Park p, ParkBeheerContext ctx)
        {
            try
            {
                ParkEF park = ctx.Park.Find(p.Id);

                if (park != null)
                {
                    // Update the existing Park
                    park.Naam = p.Naam;
                    park.Locatie = p.Locatie;
                }
                else
                {
                    // Create a new Park if it doesn't exist
                    park = new ParkEF(p.Id, p.Naam, p.Locatie);
                    ctx.Park.Add(park);
                }

                return park;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapHuurderEF - MapToDB", ex);
            }
        }
    }
}
