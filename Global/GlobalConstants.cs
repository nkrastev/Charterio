namespace Charterio.Global
{
    public static class GlobalConstants
    {
        public const string PaxTitleMr = "MR";
        public const string PaxTitleMrs = "MRS";
        public const string PaxTitleChd = "CHD";
        public const string PaxTitleInf = "INF";

        public const string ErrorEmptyOrWrongDob = "Date of birth field is required for infants in the format DD-MM-YYYY";

        public const string ErrorDobIsNotDobOfChild = "Infants are less than 2 years old. The Date you've entered is wrong.";

        public const string SystemName = "Charterio";

        public const string SystemEmail = "info@charterio.com";

        public const string AdministratorRoleName = "Administrator";

        public const string CustomerRoleName = "Customer";

        public const string ErrorSameAirports = "You cannot search flight from/to same airports. Please change departure or arrival destination";

        public const string ErrorStartApt = "Departure destination is invalid. Please select another departure destination.";

        public const string ErrorEndApt = "Arrival destination is invalid. Please select another arrival destination.";

        public const string ErrorStartDate = "Start interval date is invalid. Please select or enter correct start date.";

        public const string ErrorEndDate = "End interval date is invalid. Please select or enter correct end date.";

        public const string ErrorStartDateAfterEndDate = "End date is before Start date, or they are the same.";

        public const string ErrorSomeOfDateIsTodayOrInThePast = "Start or End date is Today or in the past. Please change dates.";

        public const string ErrorNotEnoughtAvailableSeats = "Not enought available seats for this flight. Please restart your booking.";

        public const string ErrorFlightDoesNotExists = "The selected flight does not exists. Please restart your booking.";

        public const string StripePaymentDescription = "Flight ticket from Charterio.com";

        public const string AnswerToYourQuestionSubject = "Answer to your question at Charterio.com";

        public const string AdminSameAirports = "You cannot add offer with same departure and arrival airports.";

        public const string AdminDatesInPast = "Start or End date cannot be in the past.";

        public const string AdminEndBeforeStart = "End date cannot be before Start date.";

        public const string MsgSearchByTicketCode = "Only tickets from last 50 days are showed.";

        public const int ItemsPerPage = 10;

        public const string QuestionAnswered = "Answered";
        public const string QuestionNotAnswered = "Not Answered";
        public const string SendGridNoSubjectAndMessage = "Subject and message should be provided.";
        public const string DataForTicketCode = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public const string UptimeApiError = "External Api Error";
    }
}
