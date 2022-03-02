namespace Charterio.Services.Data.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Web.ViewModels.Administration.Airport;

    public class AirportService : IAirportService
    {
        private readonly ApplicationDbContext db;

        public AirportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(AirportAddViewModel model)
        {
            var airport = new Charterio.Data.Models.Airport
            {
                Name = model.Name,
                IataCode = model.IataCode,
                UtcPosition = model.UtcPosition,
                Latitude = model.Latitude,
                Longtitude = model.Longtitude,
            };

            this.db.Airports.Add(airport);
            this.db.SaveChanges();
        }

        public void Edit(AirportViewModel model)
        {
            var airport = this.db.Airports.Where(x => x.Id == model.Id).FirstOrDefault();
            if (airport != null)
            {
                airport.Name = model.Name;
                airport.IataCode = model.IataCode;
                airport.UtcPosition = model.UtcPosition;
                airport.Latitude = model.Latitude;
                airport.Longtitude = model.Longtitude;
                this.db.SaveChanges();
            }
        }

        public List<AirportViewModel> GetAll()
        {
            var list = this.db.Airports.Select(x => new AirportViewModel
            {
                Id = x.Id,
                Name = x.Name,
                IataCode = x.IataCode,
                UtcPosition = x.UtcPosition,
                Latitude = x.Latitude,
                Longtitude = x.Longtitude,
            })
                .OrderBy(x => x.IataCode)
                .ToList();

            return list;
        }

        public AirportViewModel GetById(int id)
        {
            var airport = this.db.Airports.Where(x => x.Id == id).Select(x => new AirportViewModel
            {
                Id = x.Id,
                Name = x.Name,
                IataCode = x.IataCode,
                UtcPosition = x.UtcPosition,
                Latitude = x.Latitude,
                Longtitude = x.Longtitude,
            }).FirstOrDefault();
            if (airport != null)
            {
                return airport;
            }
            else
            {
                return null;
            }
        }
    }
}
