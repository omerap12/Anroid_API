using Web_API.Models;

namespace Web_API.Services
{
    public interface IContactService
    {
        public List<Contact> GetAllContacts();

        public List<Contact> GetContacts(string user_id);
        public void CreateNewContact(string add_to, string id, string name, string server);

        public Contact Get(string id);

        public void Delete(string user_name, string id);

        public void Edit(string id, string user_name, string password, string server);
        public List<Message> GetMessagesBetweenUsers(string user_name, string id);

        public void SendMessageToOther(string from, string to, string content);

        public void SendMessageToMe(string from, string to, string content);
        public Message GetSpecificMessage(string user_name, string other_user_id, string message_id);

        public void EditSpecificMessage(string user_name, string other_user_id, string message_id, string content);

        public void DeleteSpecificMessage(string user_name, string other_user_id, string message_id);

        public bool CheckUserInDB(string user_name, string password);


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
