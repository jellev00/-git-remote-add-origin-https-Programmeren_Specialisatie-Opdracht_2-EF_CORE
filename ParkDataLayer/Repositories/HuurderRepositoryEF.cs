using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using ParkDataLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ParkDataLayer.Repositories
{
    public class HuurderRepositoryEF : IHuurderRepository
    {
        private readonly ParkBeheerContext ctx;

        public HuurderRepositoryEF(string connectionString)
        {
            this.ctx = new ParkBeheerContext(connectionString);
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public Huurder GeefHuurder(int id)
        {
            try
            {
                return MapHuurderEF.MapToDomain(ctx.Huurder.Where(x => x.Id == id).AsNoTracking().FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefHuurder", ex);
            }
        }

        public List<Huurder> GeefHuurders(string naam)
        {
            try
            {
                return ctx.Huurder.Where(x => x.Naam == naam).Select(x => MapHuurderEF.MapToDomain(x)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefHuurders", ex);
            }
        }

        public bool HeeftHuurder(string naam, Contactgegevens contact)
        {
            try
            {
                return ctx.Huurder.Any(x => x.Naam == naam && x.Email == contact.Email && x.Telefoon == contact.Tel && x.Adres == contact.Adres);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftHuurder", ex);
            }
        }

        public bool HeeftHuurder(int id)
        {
            try
            {
                return ctx.Huurder.Any(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftHuurder", ex);
            }
        }

        public void UpdateHuurder(Huurder huurder)
        {
            try
            {
                ctx.Huurder.Update(MapHuurderEF.MapToDB(huurder, ctx));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateHuurder", ex);
            }
        }

        public Huurder VoegHuurderToe(Huurder h)
        {
            try
            {
                ctx.Huurder.Add(MapHuurderEF.MapToDB(h, ctx));
                SaveAndClear();
                return h;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegHuurderToe", ex);
            }
        }
    }
}
