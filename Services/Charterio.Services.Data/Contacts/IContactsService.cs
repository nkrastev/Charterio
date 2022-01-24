namespace Charterio.Services.Data.Contacts
{
    using Charterio.Web.ViewModels.Contacts;

    public interface IContactsService
    {
        void SaveUserQuestion(ContactsViewModel input);

        RegisteredUserContactsViewModel GetAspNetUserByUserName(string userName);
    }
}
