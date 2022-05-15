using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace Web_API.Models
{
    public class Contact
    {
        public DateTime LastDate { get; set; }
        private List<Contact> Contacts { get; set; }
        private List<Conversation> Conversations { get; set; }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        public int ContactId { get; set; }

        public string CreatedDate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Server { get; set; }

        public string Last { get; set; }

        public Contact(string id, string name, string password, string server)
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
            Contacts.Remove(Contacts.Find(x => x.Id == id));
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

    }
}
