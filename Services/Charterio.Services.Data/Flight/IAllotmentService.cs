namespace Charterio.Services.Data.Flight
{
    public interface IAllotmentService
    {
        bool AreSeatsAvailable(int offerId, int neededSeats);

        int SoldTicketsPaxCount(int offerId);

        int GetInitialAllotment(int offerId);

        int GetAvailableSeatsForOffer(int offerId);
    }
}
