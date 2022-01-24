namespace Charterio.Services.Data.Flight
{
    public interface IAllotmentService
    {
        bool AreSeatsAvailable(int offerId, int neededSeats);
    }
}
