namespace Charterio.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Charterio";

        public const string AdministratorRoleName = "Administrator";

        public const string ErrorSameAirports = "You cannot search flight from/to same airports. Please change departure or arrival destination";

        public const string ErrorStartApt = "Departure destination is invalid. Please select another departure destination.";

        public const string ErrorEndApt = "Arrival destination is invalid. Please select another arrival destination.";

        public const string ErrorStartDate = "Start interval date is invalid. Please select or enter correct start date.";

        public const string ErrorEndDate = "End interval date is invalid. Please select or enter correct end date.";

        public const string ErrorStartDateAfterEndDate = "End date is before Start date, or they are the same.";

        public const string ErrorSomeOfDateIsTodayOrInThePast = "Start or End date is Today or in the past. Please change dates.";
    }

}
