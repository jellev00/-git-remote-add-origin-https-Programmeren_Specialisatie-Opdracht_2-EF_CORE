using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class ContractenRepositoryEF : IContractenRepository
    {
        private readonly ParkBeheerContext ctx;

        public ContractenRepositoryEF(string connectionString)
        {
            this.ctx = new ParkBeheerContext(connectionString);
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public void AnnuleerContract(Huurcontract contract)
        {
            try
            {
                ctx.HuurContract.Remove(new HuurContractEF() { Id = contract.Id });
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("AnnuleerContract", ex);
            }
        }

        public Huurcontract GeefContract(string id)
        {
            try
            {
                return MapHuurContractEF.MapToDomain(ctx.HuurContract.Where(x => x.Id == id).Include(x => x.Huurder).Include(x => x.Huis).ThenInclude(h => h.Park).AsNoTracking().FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefContract", ex);
            }
        }

        public List<Huurcontract> GeefContracten(DateTime dtBegin, DateTime? dtEinde)
        {
            try
            {
                //return ctx.HuurContract.Where(x => x.StartDatum == dtBegin).Select(x => MapHuurContractEF.MapToDomain(x)).ToList();

                // IQueryable vertegenwoordigt een doorzoekbare verzameling, waardoor uitgestelde uitvoering mogelijk is.
                IQueryable<HuurContractEF> query = ctx.HuurContract
                    .Include(x => x.Huurder)  // Include Huurder in query.
                    .Include(x => x.Huis)
                    .ThenInclude(h => h.Park)// Include Huis in query.
                    .AsNoTracking();          // Geeft aan dat de resultaten niet hoeven te worden bijgehouden voor wijzigingen.

                // filter op startDatum.
                query = query.Where(x => x.StartDatum == dtBegin);

                // filter op eindDatum als deze een waarde heeft.
                if (dtEinde.HasValue)
                {
                    query = query.Where(x => x.EindDatum <= dtEinde.Value);
                }

                // voer query uit en mapt dit naar domain.
                List<Huurcontract> contracten = query.ToList().Select(MapHuurContractEF.MapToDomain).ToList();

                // geeft een lijst van huurcontracten terug.
                return contracten;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefContracten", ex);
            }
        }

        public bool HeeftContract(DateTime startDatum, int huurderid, int huisid)
        {
            try
            {
                return ctx.HuurContract.Any(x => x.StartDatum == startDatum && x.Huurder.Id == huurderid && x.Huis.Id == huisid);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftContract", ex);
            }
        }

        public bool HeeftContract(string id)
        {
            try
            {
                return ctx.HuurContract.Any(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftContract", ex);
            }
        }

        public void UpdateContract(Huurcontract contract)
        {
            try
            {
                ctx.HuurContract.Update(MapHuurContractEF.MapToDB(contract, ctx));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateContract", ex);
            }
        }

        public void VoegContractToe(Huurcontract contract)
        {
            try
            {
                HuurContractEF huurContract = MapHuurContractEF.MapToDB(contract, ctx);
                ctx.HuurContract.Add(huurContract);
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegContractToe", ex);
            }
        }
    }
}
