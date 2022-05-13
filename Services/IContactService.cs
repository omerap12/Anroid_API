using Web_API.Models;

namespace Web_API.Services
{
    public interface IContactService
    {
        public List<Contact> GetAllContacts();

        public List<Contact> GetContacts(string user_id);
        public void CreateNewContact(string add_to, string id, string name, string server);

        public Contact Get(string id);

        public void Delete(string id);

        public void Edit(string id, string user_name, string password, string server);


        /*public void Delete(string id);

        public void Create(string name, string server);

        public void Add(Contact contact);

        public void Update(string name, string server);

        public List<Message> GetMessages(string id);

        public void AddMessage(string id, string content);
        public Message GetMessageWithId_2FromId_1(string user_id,string message_id);

        public void DeleteMessage(string user_id, string message_id);

        public void SendNewMessage(string user_id_from, string user_id_to, string content);*/
    }
}
