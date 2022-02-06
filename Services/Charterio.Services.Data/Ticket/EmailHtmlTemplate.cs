namespace Charterio.Services.Data.Ticket
{
    using System.Collections.Generic;
    using System.Text;

    using Charterio.Web.ViewModels;
    using Charterio.Web.ViewModels.Ticket;

    internal class EmailHtmlTemplate
    {
        public EmailHtmlTemplate()
        {
        }

        public string GenerateTemplate(string ticketCode, List<TicketPaxViewModel> paxList, FlightViewModel flight)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\" />");
            sb.AppendLine("<title>Ticket</title>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<body style=\"background - color: #ebebeb; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none; font-size:15px; font-family:Arial, Helvetica, sans-serif;\">");
            sb.AppendLine("<table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100 % \">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");
            sb.AppendLine("<table align=\"center\" border=\"0\" cellpadding=\"2\" cellspacing=\"2\" class=\"row - content stack\" style=\"mso - table - lspace: 0pt; mso - table - rspace: 0pt; background - color: #ffffff; color: #000000; width: 680px; text-align:center;\" width=\"680\">");
            sb.AppendLine("<tr><td><img src=\"https://charterio.com/static/TicketConfirmation.jpg\" /></td></tr>");
            sb.AppendLine("<tr><td><p>Tel: in the UK: 0888 11 22 33 44 (local rate).<br />Please note this number is for existing bookings only.Please have your booking code ready.</p></td></tr>");
            sb.AppendLine($"<h1>{ticketCode}</h1>");
            sb.AppendLine("<tr><td><hr /></td></tr>");
            sb.AppendLine("<tr><td><h3>Passengers info:</h3></td></tr>");

            foreach (var pax in paxList)
            {
                sb.AppendLine($"<tr><td>{pax.PaxTitle} {pax.PaxFirstName} {pax.PaxLastName}</td></tr>");
            }

            sb.AppendLine("<tr><td><hr /></td></tr>");
            sb.AppendLine($"<tr><td><h3>Flight number: {flight.FlightNumber}</h3></td></tr>");
            sb.AppendLine($"<tr><td><strong>Departs from:</strong> {flight.Start} <strong>at</strong> {flight.StartDate.AddHours(flight.StartUtcPosition)}</td></tr>");
            sb.AppendLine($"<tr><td><strong>Arrives at:</strong> {flight.End} <strong>at</strong> {flight.EndDate.AddHours(flight.EndUtcPosition)}</td></tr>");
            sb.AppendLine("<tr><td><hr /></td></tr>");
            sb.AppendLine("<tr><td><p style=\"text - align:justify; font - size:12px; padding - left:5px; padding - right:5px; \">When making a reservation, the passenger is obliged to provide the Charterer with his / her telephone number and e-mail address at which the Charterer will be able to inform the passenger on short notice about important matters concerning reservation . The Passenger is liable for correctness and functionality of the contacts provided to the Carrier The Charterer or its authorized representative will register the passenger's reservation and will provide him with an E-ticket either via e-mail to the e-mail address provided by the passenger . The contract is considered as concluded at the moment of payment of the fare. Children and infant age is determined at the time of travel, not at booking time.</p></td></tr>");
            sb.AppendLine("</table></td></tr></table></body></html>");
            return sb.ToString();
        }
    }
}
