# Ticket Management System For Charter Flights
Live Demo can be checked at https://charterio.com (Database is automatically rolled back every 12 hours)

## Overview
Charterio is a ready to use application for ticket management for charter flights which is easy to use and has a simple user-friendly interface. Programming languages, technologies, libraries and frameworks: C#, ASP.NET Core, Entity Framework Core, JS, Bootstrap, jQuery. The app has integration with: Stripe and Braintree payment gateways, SendGrid and MailChimp for mail support and list management, UptimeRobot for monitoring of the uptime. Google Analytics and Google Maps integrated in the UI. Very simple Web Api with information about the available flights, dates and prices can be found at: domain/api/available-flights. Live demo is running on Windows Server 2022 Standard with MSSQL 2019. The application does not require registration for checking the prices of the tickets but simple one is needed in order to purchase them.

## Functionallity and Test Accounts

**Stripe test card:** 4242 4242 4242 4242 / Any future date, Any 3 digit CVC

**Braintree test card:** 4111 1111 1111 1111 / Any future date, Any 3 digit CVC

**Customer:** u: user@charterio.com p: 00000000

**Administrator:** u: administrator@charterio.com p: 00000000

**Api** https://charterio.com/api/available-flights

## Screenshots
<p align="center">
<img src="https://res.cloudinary.com/charterio/image/upload/v1647886430/assets/db_scheme_fa9kqn.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1648308921/assets/tests_flfmqi.jpg" />  
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901689/assets/homepage_ljnn4u.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901690/assets/search-result_ntwsip.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901689/assets/schedule_obho0z.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901689/assets/faq_ywdrgw.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901689/assets/contacts_hjixme.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901689/assets/flight-details_j1emsq.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901688/assets/confirm-and-pay_i9lkk1.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901689/assets/my-profile_heoq50.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901688/assets/404_xvwcwd.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901688/assets/admin-airport_knmsgh.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901688/assets/admin-airplane_ujcaap.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901688/assets/admin-flight_lv8byp.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901688/assets/admin-offer_g1azwi.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647901688/assets/admin-paymentmethods_vyc8t4.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647902771/assets/admin-questions_llirea.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647902771/assets/admin-answer_vci2v7.jpg" />
<img src="https://res.cloudinary.com/charterio/image/upload/v1647902262/assets/mobile_zfjcju.jpg" />
</p>

## Credits
ASP.NET Core Template - [Nikolay Kostov](https://github.com/NikolayIT), [Stoyan Shopov](https://github.com/StoyanShopov), [Vladislav Karamfilov](https://github.com/vladislav-karamfilov)

Project images - [Unsplash.com](https://unsplash.com/)
