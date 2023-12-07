using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class HuizenRepositoryEF : IHuizenRepository
    {
        private readonly ParkBeheerContext ctx;

        public HuizenRepositoryEF(string connectionString)
        {
            this.ctx = new ParkBeheerContext(connectionString);
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public Huis GeefHuis(int id)
        {
            try
            {
                return MapHuisEF.MapToDomain(ctx.Huis.Where(x => x.Id == id).Include(x => x.Park).Include(x => x.HuurContracten).AsNoTracking().FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefHuis", ex);
            }
        }

        public bool HeeftHuis(string straat, int nummer, Park park)
        {
            try
            {
                return ctx.Huis.Any(x => x.Straat == straat && x.Nummer == nummer && x.Park.Id == park.Id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftHuis", ex);
            }
        }

        public bool HeeftHuis(int id)
        {
            try
            {
                return ctx.Huis.Any(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftHuis", ex);
            }
        }

        public void UpdateHuis(Huis huis)
        {
            try
            {
                ctx.Huis.Update(MapHuisEF.MapToDB(huis, ctx));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateHuis", ex);
            }
        }

        public Huis VoegHuisToe(Huis h)
        {
            try
            {
                HuisEF huis = MapHuisEF.MapToDB(h, ctx);
                ctx.Huis.Add(huis);
                SaveAndClear();
                return h;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegHuisToe", ex);
            }
        }
    }
}
