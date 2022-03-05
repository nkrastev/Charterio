namespace Charterio.Services.Payment
{
    using System.Collections.Generic;

    using Charterio.Data;
    using Charterio.Web.ViewModels.Administration.Payment;

    public class PaymentAdministrationService : IPaymentAdministrationService
    {
        private readonly ApplicationDbContext db;

        public PaymentAdministrationService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<PaymentViewModel> GetAll()
        {
            var model = this.db.PaymentMethods.Select(x => new PaymentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
            })
            .ToList();
            return model;
        }

        public void DisableMethodById(int id)
        {
            // Check if method exists and if it is enabled
            var targetMethod = this.db.PaymentMethods.Where(x => x.Id == id && x.IsActive).FirstOrDefault();

            if (targetMethod != null)
            {
                targetMethod.IsActive = false;
                this.db.SaveChanges();
            }
        }

        public void EnableMethodById(int id)
        {
            // Check if method exists and if it is disabled
            var targetMethod = this.db.PaymentMethods.Where(x => x.Id == id && !x.IsActive).FirstOrDefault();

            if (targetMethod != null)
            {
                targetMethod.IsActive = true;
                this.db.SaveChanges();
            }
        }
    }
}
