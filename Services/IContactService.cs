using Web_API.Models;

namespace Web_API.Services
{
    public interface IContactService
    {
        public List<Contact> GetAll();

        public Contact Get(string id);

        public void Delete(string id);

        public void Create(string name, string server);

        public void Add(Contact contact);

        public void Update(string name, string server);

        public List<Message> GetMessages(string id);

        public void AddMessage(string id, string content);
        public Message GetMessageWithId_2FromId_1(string user_id,string message_id);
    }
}
