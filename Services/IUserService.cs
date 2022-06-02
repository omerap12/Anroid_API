using Web_API.Models;
using WebApi.Models;
using WebApi.View;

namespace Web_API.Services
{
    public interface IUserService
    {
        public List<User> GetAllContacts();

        public List<ContactFirstAPI> GetAllContactsAPI();

        public List<ContactFirstAPI> GetContacts(string user_id);
        public void CreateNewContact(string add_to, string id, string name, string server);

        public User Get(string id);

        public void Delete(string user_name, string id);

        public void Edit(string id, string user_name, string server);
        public List<Message> GetMessagesBetweenUsers(string user_name, string id);

        public void SendMessageToOther(string from, string to, string content);

        public void SendMessageToMe(string from, string to, string content);
        public Message GetSpecificMessage(string user_name, string other_user_id, string message_id);

        public void EditSpecificMessage(string user_name, string other_user_id, string message_id, string content);

        public void DeleteSpecificMessage(string user_name, string other_user_id, string message_id);

        public bool CheckUserInDB(string user_name, string password);

        public string GetUserServer(string user_name);

        public bool Check_if_friends(User user_name, string other_user_name);
        public ContactFirstAPI GetApiContact(string username, string id);

        public void AddNewFromOtherServer(string from,string to,string server);
    }
}
