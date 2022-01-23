namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data.Models;

    internal class FaqsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Faqs.Any())
            {
                return;
            }

            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "I've forgotten to write my middle name when booking the ticket. Is this a problem?",
                    Answer = "Middle names aren't necessary for a valid flight ticket. It is important that correctly spelled first and last names are present on the ticket. Please double check that the names are spelled in the same way as in the passenger's passport.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "Can I change the date on my reservation?",
                    Answer = "Dependin on availability, it is sometimes possible to change the date of your flight against a certain surcharge.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a cancellation and how is the refund proceeded?",
                    Answer = "There is no refund for a cancelled flight ticket. Please check full cancellation policy under the \"Terms and Conditions\" menu.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "When do I receive my tickets?",
                    Answer = "A confirmation of the booking is sent to the email you have stated while booking. Please print out this confirmation and bring it with you to the airport check-in desk.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a booking?",
                    Answer = "You can only book a flight via the website. After registration you will be able to see the book now button on the flight page. Our prices will apply to an online booking only.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I pay for the flight?",
                    Answer = "The displayed fares are valid with a credit/debit card payment (VISA or MASTERCARD).",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "I've forgotten to write my middle name when booking the ticket. Is this a problem?",
                    Answer = "Middle names aren't necessary for a valid flight ticket. It is important that correctly spelled first and last names are present on the ticket. Please double check that the names are spelled in the same way as in the passenger's passport.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "Can I change the date on my reservation?",
                    Answer = "Dependin on availability, it is sometimes possible to change the date of your flight against a certain surcharge.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a cancellation and how is the refund proceeded?",
                    Answer = "There is no refund for a cancelled flight ticket. Please check full cancellation policy under the \"Terms and Conditions\" menu.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "When do I receive my tickets?",
                    Answer = "A confirmation of the booking is sent to the email you have stated while booking. Please print out this confirmation and bring it with you to the airport check-in desk.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a booking?",
                    Answer = "You can only book a flight via the website. After registration you will be able to see the book now button on the flight page. Our prices will apply to an online booking only.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I pay for the flight?",
                    Answer = "The displayed fares are valid with a credit/debit card payment (VISA or MASTERCARD).",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "I've forgotten to write my middle name when booking the ticket. Is this a problem?",
                    Answer = "Middle names aren't necessary for a valid flight ticket. It is important that correctly spelled first and last names are present on the ticket. Please double check that the names are spelled in the same way as in the passenger's passport.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "Can I change the date on my reservation?",
                    Answer = "Dependin on availability, it is sometimes possible to change the date of your flight against a certain surcharge.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a cancellation and how is the refund proceeded?",
                    Answer = "There is no refund for a cancelled flight ticket. Please check full cancellation policy under the \"Terms and Conditions\" menu.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "When do I receive my tickets?",
                    Answer = "A confirmation of the booking is sent to the email you have stated while booking. Please print out this confirmation and bring it with you to the airport check-in desk.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a booking?",
                    Answer = "You can only book a flight via the website. After registration you will be able to see the book now button on the flight page. Our prices will apply to an online booking only.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I pay for the flight?",
                    Answer = "The displayed fares are valid with a credit/debit card payment (VISA or MASTERCARD).",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a cancellation and how is the refund proceeded?",
                    Answer = "There is no refund for a cancelled flight ticket. Please check full cancellation policy under the \"Terms and Conditions\" menu.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "When do I receive my tickets?",
                    Answer = "A confirmation of the booking is sent to the email you have stated while booking. Please print out this confirmation and bring it with you to the airport check-in desk.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a booking?",
                    Answer = "You can only book a flight via the website. After registration you will be able to see the book now button on the flight page. Our prices will apply to an online booking only.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I pay for the flight?",
                    Answer = "The displayed fares are valid with a credit/debit card payment (VISA or MASTERCARD).",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "I've forgotten to write my middle name when booking the ticket. Is this a problem?",
                    Answer = "Middle names aren't necessary for a valid flight ticket. It is important that correctly spelled first and last names are present on the ticket. Please double check that the names are spelled in the same way as in the passenger's passport.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "Can I change the date on my reservation?",
                    Answer = "Dependin on availability, it is sometimes possible to change the date of your flight against a certain surcharge.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a cancellation and how is the refund proceeded?",
                    Answer = "There is no refund for a cancelled flight ticket. Please check full cancellation policy under the \"Terms and Conditions\" menu.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "When do I receive my tickets?",
                    Answer = "A confirmation of the booking is sent to the email you have stated while booking. Please print out this confirmation and bring it with you to the airport check-in desk.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a booking?",
                    Answer = "You can only book a flight via the website. After registration you will be able to see the book now button on the flight page. Our prices will apply to an online booking only.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I pay for the flight?",
                    Answer = "The displayed fares are valid with a credit/debit card payment (VISA or MASTERCARD).",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a cancellation and how is the refund proceeded?",
                    Answer = "There is no refund for a cancelled flight ticket. Please check full cancellation policy under the \"Terms and Conditions\" menu.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "When do I receive my tickets?",
                    Answer = "A confirmation of the booking is sent to the email you have stated while booking. Please print out this confirmation and bring it with you to the airport check-in desk.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a booking?",
                    Answer = "You can only book a flight via the website. After registration you will be able to see the book now button on the flight page. Our prices will apply to an online booking only.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I pay for the flight?",
                    Answer = "The displayed fares are valid with a credit/debit card payment (VISA or MASTERCARD).",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "I've forgotten to write my middle name when booking the ticket. Is this a problem?",
                    Answer = "Middle names aren't necessary for a valid flight ticket. It is important that correctly spelled first and last names are present on the ticket. Please double check that the names are spelled in the same way as in the passenger's passport.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "Can I change the date on my reservation?",
                    Answer = "Dependin on availability, it is sometimes possible to change the date of your flight against a certain surcharge.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a cancellation and how is the refund proceeded?",
                    Answer = "There is no refund for a cancelled flight ticket. Please check full cancellation policy under the \"Terms and Conditions\" menu.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "When do I receive my tickets?",
                    Answer = "A confirmation of the booking is sent to the email you have stated while booking. Please print out this confirmation and bring it with you to the airport check-in desk.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I make a booking?",
                    Answer = "You can only book a flight via the website. After registration you will be able to see the book now button on the flight page. Our prices will apply to an online booking only.",
                });
            await dbContext.Faqs.AddAsync(
                new Faq
                {
                    Question = "How can I pay for the flight?",
                    Answer = "The displayed fares are valid with a credit/debit card payment (VISA or MASTERCARD).",
                });
        }
    }
}
