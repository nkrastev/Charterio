namespace Charterio.Services.Payment
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Administration.Payment;

    public interface IPaymentAdministrationService
    {
        ICollection<PaymentViewModel> GetAll();

        void DisableMethodById(int id);

        void EnableMethodById(int id);
    }
}
