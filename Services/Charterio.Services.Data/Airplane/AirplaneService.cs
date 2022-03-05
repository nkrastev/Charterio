namespace Charterio.Services.Data.Airplane
{
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Web.ViewModels.Administration.Airplane;

    public class AirplaneService : IAirplaneService
    {
        private readonly ApplicationDbContext db;

        public AirplaneService(ApplicationDbContext db)
        {
            this.db = db;
        }

        // Administration services
        public void Delete(int id)
        {
            var plane = this.db.Planes.Where(x => x.Id == id).FirstOrDefault();
            if (plane != null)
            {
                this.db.Planes.Remove(plane);
                this.db.SaveChanges();
            }
        }

        public void Edit(AirplaneViewModel model)
        {
            var plane = this.db.Planes.Where(x => x.Id == model.Id).FirstOrDefault();
            if (plane != null)
            {
                plane.Model = model.Model;
                this.db.SaveChanges();
            }
        }

        public AirplaneViewModel GetById(int id)
        {
            var plane = this.db.Planes.Where(x => x.Id == id).Select(x => new AirplaneViewModel
            {
                Id = x.Id,
                Model = x.Model,
            }).FirstOrDefault();
            if (plane != null)
            {
                return plane;
            }
            else
            {
                return null;
            }
        }

        public void Add(AirplaneAddViewModel model)
        {
            var airplane = new Plane
            {
                Model = model.Model,
            };

            this.db.Planes.Add(airplane);
            this.db.SaveChanges();
        }

        public List<AirplaneViewModel> GetAll()
        {
            var planes = this.db.Planes.Select(x => new AirplaneViewModel
            {
                Id = x.Id,
                Model = x.Model,
            }).ToList();
            return planes;
        }
    }
}
