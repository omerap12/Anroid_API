using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models;
using WebApi.View;
using WebShop;

namespace Web_API.Models
{
    public class User
    {
        public DateTime? LastDate { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Conversation> Conversations { get; set; }

        public User RefContact { get; set; }
        [ForeignKey("Id")]
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        public string ContactId { get; set; }

        public string CreatedDate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Server { get; set; }

        public string Last { get; set; }

        public bool IsFriendOfMe(string id)
        {
            Contact contact = Contacts.Find(x => x.ContactId == id);
            if (contact == null)
                return false;
            return true;
        }

        public User(string id, string name, string password, string server)
        {
            this.Id = id;
            this.Name = name;
            this.Contacts = new List<Contact>();
            this.Password = password;
            this.Server = server;
            this.Conversations = new List<Conversation>();
            this.LastDate = DateTime.Now;
            this.Last = null;

        }
        public User()
        {
        }

        public void AddContacts(Contact contact)
        {
            this.Contacts.Add(contact);
        }
        public void AddConversation(Conversation conversation)
        {
            this.Conversations.Add(conversation);
        }
        public List<Contact> GetContactsList()
        {
            return this.Contacts;
        }
        public List<Conversation> GetConversations ()
        {
            return this.Conversations;
        }

        public List<Message> GetMessagesFromUser(string id)
        {
            for (int i = 0; i< Conversations.Count; i++)
            {
                if (Conversations[i].IsMe(id) == true)
                {
                    return Conversations[i].GetAllMessages();
                }
            }
            return null;
        }

        public Message GetSpecificMessageFromUser(string other_user_id, string id)
        {
            List<Message> messages = GetMessagesFromUser(other_user_id);
            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i].Id == id)
                    return messages[i];
            }
            return null;
        }

        public void Delete_user(string id)
        {
            using (var d = new ItemsContext())
            {
                d.Users.Remove(d.Users.ToList().Find(x => x.Id == id));
            }
        }

        public void setMessage(string to, string content, bool sent)
        {
            for (int i = 0; i < Conversations.Count; i++)
            {
                if (Conversations[i].IsMe(to) == true)
                {
                    Conversations[i].AddMessage(new Message(content, sent));
                }
            }
            this.Last = content;
        }
        public void EditSpecificMessage(string other_user_id, string message_id, string content)
        {
            Message message = GetSpecificMessageFromUser(other_user_id, message_id);
            message.Content = content;

        }
        public void DeleteSpecificMessage(string other_user_id, string message_id)
        {
            List<Message> messages = GetMessagesFromUser(other_user_id);
            messages.Remove(messages.Find(m => m.Id == message_id));
        }

        public string GetLast(string other_user)
        {
            Conversation conversation = Conversations.Find(c => c.Contacts.Any(x => x.Id == other_user));
            if (conversation == null)
                return null;
            return conversation.last;
        }

        public string GetLastDate(string other_user)
        {
            Conversation conversation = Conversations.Find(c => c.Contacts.Any(x => x.Id == other_user));
            if (conversation == null)
                return null;
            return conversation.lastdate;
        }

        public List<ContactFirstAPI> GetContacts()
        {
            List<ContactFirstAPI> contacts = new List<ContactFirstAPI>();
            foreach (Conversation conversation in Conversations)
            {
                string from = conversation.Contacts.First().Id;
                string to = conversation.Contacts[1].Id;
                string name_to_add = "";
                string Last = "";
                string server_to_add;
                if (from == Id)
                    name_to_add = to;
                else
                    name_to_add= from;
                Last = conversation.last;
                contacts.Add(new ContactFirstAPI(name_to_add, name_to_add, null, Last, conversation.lastdate));
            }
            return contacts;
        }

    }
}
