namespace Charterio.Services.Data.Contacts
{
    using Charterio.Web.ViewModels.Contacts;

    public interface IContactService
    {
        void SaveUserQuestion(ContactsViewModel input);

        RegisteredUserContactsViewModel GetAspNetUserByUserName(string userName);
    }
}
